using Microsoft.AspNetCore.Identity;

namespace prjAllShow.Backend.Models.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public string Description { get; set; }
    }
}
