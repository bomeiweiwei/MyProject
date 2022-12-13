using AllShow.Models.Identity;
using AllShowDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.Interface
{
    public interface IApplicationUserService
    {
        ApplicationUser Authentication(string useremail, string password);
        List<UserDTO> GetAll();
        List<UserDTO> GetUserRoles(int Id);
        UserDTO FindOne(int Id);
        ApplicationUser FindOne(string account);
    }
}
