using AllShow.Models.Identity;
using AllShowDTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.Interface
{
    public interface ITokenService
    {
        Task<AuthResult> BuildToken(string key, ApplicationUser user, string[] roleNames);
        Task<AuthResult> RefreshTokenAsync(string token, string refreshToken, int userId);
        bool IsTokenValid(string key, string token);
    }
}
