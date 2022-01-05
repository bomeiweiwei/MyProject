using AllShow.Models.Identity;
using AllShow.Interface;
using AllShowService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AllShowDTO;

namespace AllShowService
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWorksPlus _unitOfWork;
        public ApplicationUserService(IUnitOfWorksPlus unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApplicationUser Authentication(string useremail, string password)
        {
            //var defaultUser = new ApplicationUser();
            //var passwordHasher = new PasswordHasher<ApplicationUser>();
            //var hashedPassword = passwordHasher.HashPassword(defaultUser, password);

            var applicationUser = _unitOfWork.ApplicationUserRepository.Get(item => item.Email == useremail && item.PasswordHash == password).FirstOrDefault();
            return applicationUser;
        }

        public List<UserDTO> GetAll()
        {
            var alist = _unitOfWork.ApplicationUserRepository.Get();
            var rlist = _unitOfWork.ApplicationRoleRepository.Get();
            var urlist = _unitOfWork.IdentityUserRoleRepository.Get();
            var query = (from item1 in alist
                         join item2 in urlist on item1.Id equals item2.UserId
                         join item3 in rlist on item2.RoleId equals item3.Id
                         select new UserDTO
                         {
                             Id = item1.Id,
                             Name = item1.UserName,
                             Email = item1.Email,
                             Role = item3.Name
                         }).ToList();
            return query;
        }
        public List<UserDTO> GetUserRoles(int Id)
        {
            var auser = _unitOfWork.ApplicationUserRepository.Get(m => m.Id == Id);
            var rlist = _unitOfWork.ApplicationRoleRepository.Get();
            var urlist = _unitOfWork.IdentityUserRoleRepository.Get();
            var query = (from item1 in auser
                         join item2 in urlist on item1.Id equals item2.UserId
                         join item3 in rlist on item2.RoleId equals item3.Id
                         select new UserDTO
                         {
                             Id = item1.Id,
                             Name = item1.UserName,
                             Email = item1.Email,
                             Role = item3.Name
                         }).ToList();
            return query;
        }
        public UserDTO FindOne(int Id)
        {
            var auser = _unitOfWork.ApplicationUserRepository.Get(m => m.Id == Id);
            var rlist = _unitOfWork.ApplicationRoleRepository.Get();
            var urlist = _unitOfWork.IdentityUserRoleRepository.Get();
            var query = (from item1 in auser
                         join item2 in urlist on item1.Id equals item2.UserId
                         join item3 in rlist on item2.RoleId equals item3.Id
                         select new UserDTO
                         {
                             Id = item1.Id,
                             Name = item1.UserName,
                             Email = item1.Email,
                             Role = item3.Name
                         }).FirstOrDefault();
            return query ?? new UserDTO();
        }
    }
}
