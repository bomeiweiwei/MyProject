using AllShow.Models;
using AllShow.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.Interface
{
    public interface IRefreshTokenService
    {
        void CreateRefreshToken(RefreshToken refreshToken);
        void UpdateRefreshToken(RefreshToken refreshToken);
    }
}
