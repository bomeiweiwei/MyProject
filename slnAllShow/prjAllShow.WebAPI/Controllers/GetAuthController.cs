using System.Security.Cryptography;
using AllShowCommon;
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
        private readonly Jwt _jwtSettings;
        private string generatedToken = null;
        private readonly string aesKey;
        public GetAuthController(IApplicationUserService service, ITokenService tokenService, Jwt jwtSettings, IConfiguration config)
        {
            _service = service;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings;
            _config = config;
            this.aesKey = _config.GetSection("AES_Key").Value;
        }       

        [AllowAnonymous]
        [HttpPost("authentication")]
        public async Task<IActionResult> AuthenticationAsync([FromBody] UserCredential userCredential)
        {
            string aesEmail = AESUtility.AESDecryptor(userCredential.UserEmail, aesKey);
            string aesPWD = AESUtility.AESDecryptor(userCredential.Password, aesKey);

            var user = _service.Authentication(aesEmail, aesPWD);
            if (user == null)
            {
                //return Unauthorized();
                return Unauthorized(
                        new TokenResult()
                        {
                            Errors = new[] { "User not exist" }
                        });
                //return Ok(new TokenResult { Errors = new [] { "User not exist" } });
            }
            else
            {
                string[] roleNames = _service.GetUserRoles(user.Id).Select(m => m.Role).ToArray();
                //generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), user, roleNames);
                AuthResult authResult = await _tokenService.BuildToken(_jwtSettings.Key, user, roleNames);
                generatedToken = authResult.Token;

                if (generatedToken != null)
                {                    
                    double minute = _jwtSettings.Expire_Minutes;
                    TimeSpan time = TimeSpan.FromSeconds(minute * 60);
                    return Ok(
                        new TokenResult()
                            {
                                AccessToken = authResult.Token,
                                RefreshToken= authResult.RefreshToken,
                                TokenType = "Bearer",
                                ExpiresIn = (int)time.TotalSeconds
                            });
                }
                else
                {
                    return Unauthorized(
                        new TokenResult()
                        {
                            Errors = new[] { "Build token failed" }
                        });
                }
            }
        }

        [AllowAnonymous]
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenRequest tokenRequest)
        {
            string aesEmail = AESUtility.AESDecryptor(tokenRequest.UserEmail, aesKey);
            string aesPWD = AESUtility.AESDecryptor(tokenRequest.Password, aesKey);

            var user = _service.Authentication(aesEmail, aesPWD);
            if(user == null)
            {
                //return Unauthorized();
                return Unauthorized(
                        new TokenResult()
                        {
                            Errors = new[] { "User not exist" }
                        });
            }

            var result = await _tokenService.RefreshTokenAsync(tokenRequest.Token, tokenRequest.RefreshToken, user.Id);//VerifyToken(tokenRequest);
            if (!result.Success)
            {
                return Unauthorized(
                    new TokenResult()
                    {
                        Errors = result.Errors
                    });
            }
            double minute = _jwtSettings.Expire_Minutes;
            TimeSpan time = TimeSpan.FromSeconds(minute * 60);
            return Ok(
                new TokenResult
                {
                    AccessToken = result.Token,
                    TokenType = "Bearer",
                    ExpiresIn = (int)time.TotalSeconds,
                    RefreshToken = result.RefreshToken
                });
        }
    }
}
