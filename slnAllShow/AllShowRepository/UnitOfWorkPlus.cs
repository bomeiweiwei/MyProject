using AllShow;
using AllShow.Data;
using AllShow.Interface;
using AllShow.Models.Identity;
using AllShow.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow
{
    public class UnitOfWorkPlus : IUnitOfWorksPlus
    {
        private IdentityDBContext _context;
        private GenericRepositoryPlus<ApplicationUser> _applicationUserRepository;
        private GenericRepositoryPlus<ApplicationRole> _applicationRoleRepository;
        private GenericRepositoryPlus<IdentityUserRole<int>> _IdentityUserRoleRepository;
        private GenericRepositoryPlus<RefreshToken> _refreshTokenRepository;
        public UnitOfWorkPlus(IdentityDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<ApplicationUser> ApplicationUserRepository
        {
            get
            {
                if (this._applicationUserRepository == null)
                {
                    this._applicationUserRepository = new GenericRepositoryPlus<ApplicationUser>(_context);
                }
                return _applicationUserRepository;
            }
        }

        public IGenericRepository<ApplicationRole> ApplicationRoleRepository
        {
            get
            {
                if (this._applicationRoleRepository == null)
                {
                    this._applicationRoleRepository = new GenericRepositoryPlus<ApplicationRole>(_context);
                }
                return _applicationRoleRepository;
            }
        }

        public IGenericRepository<IdentityUserRole<int>> IdentityUserRoleRepository
        {
            get
            {
                if (this._IdentityUserRoleRepository == null)
                {
                    this._IdentityUserRoleRepository = new GenericRepositoryPlus<IdentityUserRole<int>>(_context);
                }
                return _IdentityUserRoleRepository;
            }
        }

        public IGenericRepository<RefreshToken> RefreshTokenRepository
        {
            get
            {
                if (this._refreshTokenRepository == null)
                {
                    this._refreshTokenRepository = new GenericRepositoryPlus<RefreshToken>(_context);
                }
                return _refreshTokenRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
