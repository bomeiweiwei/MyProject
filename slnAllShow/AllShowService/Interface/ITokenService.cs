using AllShow.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.Interface
{
    public interface ITokenService
    {
        string BuildToken(string key, ApplicationUser user, string[] roleNames);
        bool IsTokenValid(string key, string token);
    }
}
