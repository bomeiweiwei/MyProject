using AllShow.Data;
using AllShow.Interface;
using AllShow.Models;
using AllShow.Models.Identity;
using AllShowDTO;
using AllShowService.Interface;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService
{
    public class TokenService: ITokenService
    {
        private double EXPIRY_DURATION_MINUTES = 30;
        private readonly IdentityDBContext _context;
        private readonly IApplicationUserService _service;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly Jwt _jwtSettings;
        public TokenService(IdentityDBContext context, TokenValidationParameters tokenValidationParameters, Jwt jwtSettings, IApplicationUserService service)
        {
            _context = context;
            _tokenValidationParameters = tokenValidationParameters;
            _jwtSettings = jwtSettings;
            _service = service;

            EXPIRY_DURATION_MINUTES = _jwtSettings.Expire_Minutes;
        }
        public TokenService(double minute)
        {
            EXPIRY_DURATION_MINUTES = minute;
        }
        public async Task<AuthResult> BuildToken(string key, ApplicationUser user, string[] roleNames)
        {
            List<Claim> claimLists = new List<Claim>();
            //claimLists.Add(new Claim(ClaimTypes.Name, user.UserName));
            //claimLists.Add(new Claim(ClaimTypes.Email, user.Email));
            //claimLists.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            //claimLists.Add(new Claim("Id", user.Id.ToString()));
            claimLists.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            //claimLists.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
            claimLists.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
            claimLists.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));

            foreach (var role in roleNames)
            {
                claimLists.Add(new Claim(ClaimTypes.Role, role));
            }
            var claims = claimLists.ToArray();
            
            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            var dt = DateTime.UtcNow;
            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = dt.AddMinutes(EXPIRY_DURATION_MINUTES),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            var jwtToken = tokenHandler.WriteToken(token);

            AuthResult result = new AuthResult
            {
                JwtId = token.Id,//JwtRegisteredClaimNames.Jti
                CreatedTime = dt,
                ExpireTime = tokenDescriptor.Expires.Value,
                Token = jwtToken,
                RefreshToken = RandomString(25) + Guid.NewGuid(),
                Success = true//BuildToken true
            };
            //加入至DB
            var refreshToken = new RefreshToken()
            {
                JwtId = result.JwtId,
                IsUsed = false,
                UserId = user.Id,
                AddedDate = result.CreatedTime,
                ExpiryDate = dt.AddMonths(6),//result.ExpireTime,
                IsRevoked = false,
                Token = result.RefreshToken
            };
            await _context.RefreshTokens.AddAsync(refreshToken);
            _context.SaveChanges();

            return result;
        }

        public async Task<AuthResult> RefreshTokenAsync(string token, string refreshToken, int userId)
        {
            var claimsPrincipal = GetClaimsPrincipalByToken(token);
            //Check 1: 
            if (claimsPrincipal == null)
            {
                // 無效的token...
                return new AuthResult()
                {
                    Errors = new List<string> { "1: Invalid request!" },
                };
            }

            // Will get the time stamp in unix time
            var utcExpiryDate = long.Parse(claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            // we convert the expiry date from seconds to the date
            var expDate = UnixTimeStampToDateTime(utcExpiryDate);

            //Check 2: We cannot refresh this since the token has not expired
            if (expDate > DateTime.UtcNow)
            {
                // token未過期...
                return new AuthResult()
                {
                    Errors = new List<string> { "2: Invalid request!" },
                };
            }           

            //與BuildToken加入至DB的refreshToken.Token做比較
            var storedRefreshToken = _context.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

            //Check 3: refresh token doesnt exist
            if (storedRefreshToken == null)
            {
                // 無效的refresh_token...
                return new AuthResult()
                {
                    Errors = new List<string> { "3: Invalid request!" },
                };
            }
            //Check 4: token has expired, user needs to relogin
            if (storedRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                // refresh_token已過期...
                return new AuthResult()
                {
                    Errors = new List<string> { "4: Invalid request!" },
                };
            }
            //Check 5: 
            if (storedRefreshToken.Invalidated)
            {
                // refresh_token已失效...
                return new AuthResult()
                {
                    Errors = new List<string> { "5: Invalid request!" },
                };
            }
            //Check 6: token has been used
            if (storedRefreshToken.IsUsed)
            {
                // refresh_token已使用...
                return new AuthResult()
                {
                    Errors = new List<string> { "6: Invalid request!" },
                };
            }
            //Check 7: token has been revoked
            if (storedRefreshToken.IsRevoked)
            {
                return new AuthResult()
                {
                    Errors = new List<string> { "7: Invalid request!" },
                };
            }

            var jti = claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            //Check 8: the token doenst mateched the saved token
            if (storedRefreshToken.JwtId != jti)
            {
                // refresh_token與此token不匹配...
                return new AuthResult()
                {
                    Errors = new List<string> { "8: Invalid request!" },
                };
            }

            storedRefreshToken.IsUsed = true;
            //_userDbContext.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            var user = _context.Users.Where(m => m.Id == userId).FirstOrDefault();
            var query= _service.GetUserRoles(userId).Select(m => m.Role).ToArray();
            AuthResult result = await BuildToken(_jwtSettings.Key, user, query);

            return result;
        }

        public bool IsTokenValid(string key, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidIssuer = issuer,
                    //ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
        private ClaimsPrincipal GetClaimsPrincipalByToken(string token)
        {
            try
            {
                //var config = new MapperConfiguration(
                //    cfg =>
                //    {
                //        cfg.CreateMap<TokenValidationParameters, TokenValidationParameters>();
                //    }
                //);
                //var mapper = config.CreateMapper(); // 建立 Mapper
                //var tokenValidationParameters = mapper.Map<TokenValidationParameters>(_tokenValidationParameters);
                var tokenValidationParameters = 
                    new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key)),
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = false // 不驗證過期時間！！！
                    };

                var jwtTokenHandler = new JwtSecurityTokenHandler();

                var claimsPrincipal =
                    jwtTokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                var validatedSecurityAlgorithm = validatedToken is JwtSecurityToken jwtSecurityToken
                                                 && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                     StringComparison.InvariantCultureIgnoreCase);

                return validatedSecurityAlgorithm ? claimsPrincipal : null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

       
    }
}
