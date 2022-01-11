using AllShow.Data;
using AllShow.Models;
using AllShow.Models.Identity;
using AllShowDTO;
using AllShowService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using prjAllShow.WebAPI.Infrastructure;
using prjAllShow.WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace prjAllShow.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GetAuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IApplicationUserService _service;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshService;
        private readonly IdentityDBContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private string generatedToken = null;
        public GetAuthController(IConfiguration config,IApplicationUserService service, ITokenService tokenService, IRefreshTokenService refreshService, IdentityDBContext context, TokenValidationParameters tokenValidationParameters)
        {
            _config = config;
            _service = service;
            _tokenService = tokenService;
            _refreshService = refreshService;
            _context = context;
            _tokenValidationParameters = tokenValidationParameters;
        }       

        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var user = _service.Authentication(userCredential.UserEmail, userCredential.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                string[] roleNames = _service.GetUserRoles(user.Id).Select(m => m.Role).ToArray();
                //generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), user, roleNames);
                AuthResult authResult = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), user, roleNames);
                generatedToken = authResult.Token;

                if (generatedToken != null)
                {
                    var refreshToken = new RefreshToken()
                    {
                        JwtId = authResult.JwtId,
                        IsUsed = false,
                        UserId = user.Id,
                        AddedDate = authResult.CreatedTime,
                        ExpiryDate = authResult.ExpireTime,
                        IsRevoked = false,
                        Token = authResult.Token
                    };
                    _refreshService.CreateRefreshToken(refreshToken);

                    return Ok(authResult);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        [AllowAnonymous]
        [HttpPost("refreshtoken")]
        public IActionResult RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            var res = VerifyToken(tokenRequest);
            return Ok(res);
        }

        private AuthResult VerifyToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            bool tokenlive = true;
            try
            {
                // This validation function will make sure that the token meets the validation parameters
                // and its an actual jwt token not just a random string
                var principal = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

                // Now we need to check if the token has a valid security algorithm
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Will get the time stamp in unix time
                var utcExpiryDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                // we convert the expiry date from seconds to the date
                var expDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expDate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "We cannot refresh this since the token has not expired" },
                        Success = false
                    };
                }

                // Check the token we got if its saved in the db
                var storedRefreshToken =  _context.RefreshTokens.FirstOrDefault(x => x.Token == tokenRequest.Token);

                if (storedRefreshToken == null)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "refresh token doesnt exist" },
                        Success = false
                    };
                }

                // Check the date of the saved token if it has expired
                if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has expired, user needs to relogin" },
                        Success = false
                    };
                }

                // check if the refresh token has been used
                if (storedRefreshToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has been used" },
                        Success = false
                    };
                }

                // Check if the token is revoked
                if (storedRefreshToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "token has been revoked" },
                        Success = false
                    };
                }

                // we are getting here the jwt token id
                var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                // check the id that the recieved token has against the id saved in the db
                if (storedRefreshToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>() { "the token doenst mateched the saved token" },
                        Success = false
                    };
                }
                //storedRefreshToken.IsUsed = true;
                ////_refreshService.UpdateRefreshToken(storedRefreshToken);
                //_context.RefreshTokens.Update(storedRefreshToken);
                //await _context.SaveChangesAsync();                
            }
            catch (Exception ex)
            {
                tokenlive = false;
                //return null;
            }

            if (!tokenlive)
            {
                var user = _context.Users.FirstOrDefault(m => m.Id == tokenRequest.UserId);
                string[] roleNames = _service.GetUserRoles(user.Id).Select(m => m.Role).ToArray();
                //generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), user, roleNames);
                AuthResult authResult = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), user, roleNames);
                var refreshToken = new RefreshToken()
                {
                    JwtId = authResult.JwtId,
                    IsUsed = false,
                    UserId = user.Id,
                    AddedDate = authResult.CreatedTime,
                    ExpiryDate = authResult.ExpireTime,
                    IsRevoked = false,
                    Token = authResult.Token
                };
                _refreshService.CreateRefreshToken(refreshToken);

                return new AuthResult()
                {
                    Token = authResult.Token,
                    Success = true,
                    RefreshToken = authResult.RefreshToken,
                };
                //return authResult;
            }
            else
            {
                return new AuthResult() { Success = false, Errors = new List<string> { "gg" } };
            }
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
    }
}
