using AllShow.Interface;
using AllShow.Models;
using AllShow.Models.Identity;
using AllShowService.Interface;
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
        private double EXPIRY_DURATION_MINUTES = 15;
        //private readonly IUnitOfWorksPlus _unitOfWork;
        public TokenService()
        {
        }
        public TokenService(double minute)
        {
            EXPIRY_DURATION_MINUTES = minute;
        }
        //public TokenService(IUnitOfWorksPlus unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        public AuthResult BuildToken(string key, ApplicationUser user, string[] roleNames)
        {
            List<Claim> claimLists = new List<Claim>();
            claimLists.Add(new Claim(ClaimTypes.Name, user.UserName));
            claimLists.Add(new Claim(ClaimTypes.Email, user.Email));
            claimLists.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            //claimLists.Add(new Claim("Id", user.Id.ToString()));
            //claimLists.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            //claimLists.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
            claimLists.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

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

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                UserId = user.Id,
                AddedDate = dt,
                ExpiryDate = tokenDescriptor.Expires.Value,
                Token = RandomString(25) + Guid.NewGuid()
            };
            //_refreshService.CreateRefreshToken(refreshToken);
            //await _context.RefreshTokens.AddAsync(refreshToken);
            //await _context.SaveChangesAsync();
            //_unitOfWork.RefreshTokenRepository.Insert(refreshToken);
            //_unitOfWork.SaveChanges();

            AuthResult result = new AuthResult
            {
                JwtId = refreshToken.JwtId,//JwtRegisteredClaimNames.Jti
                CreatedTime = refreshToken.AddedDate,
                ExpireTime = refreshToken.ExpiryDate,
                Token = jwtToken,
                RefreshToken = refreshToken.Token
            };
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

        public string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
