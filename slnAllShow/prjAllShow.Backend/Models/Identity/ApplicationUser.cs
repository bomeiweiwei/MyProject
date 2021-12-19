using Microsoft.AspNetCore.Identity;

namespace prjAllShow.Backend.Models.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public bool IsAdmin { get; set; }
    }
}
