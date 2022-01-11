using AllShow.Interface;
using AllShow.Models;
using AllShow.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Interface
{
    public interface IUnitOfWorksPlus
    {
        IGenericRepository<ApplicationUser> ApplicationUserRepository { get; }
        IGenericRepository<ApplicationRole> ApplicationRoleRepository { get; }
        IGenericRepository<IdentityUserRole<int>> IdentityUserRoleRepository { get; }
        IGenericRepository<RefreshToken> RefreshTokenRepository { get; }
        void SaveChanges();
    }
}
