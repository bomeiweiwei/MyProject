using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace prjAllShow.Backend.Models.Identity
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            if (principal.Identity != null)
            {
                var identity = (ClaimsIdentity)principal.Identity;

                var claims = new List<Claim>();
                if (user.IsAdmin)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, "admin"));
                    //claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, "factory"));
                    //claims.Add(new Claim(ClaimTypes.Role, "Factory"));
                }

                identity.AddClaims(claims);
            }
            return principal;
        }
    }
}
