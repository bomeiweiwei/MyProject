using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Security.Principal;

namespace AllShow.Helper
{
    public static class JWTClaimHelper
    {
        private static IHttpContextAccessor _context;
        public static void JWTClaimHelperConfigure(IHttpContextAccessor context)
        {
            _context = context;
        }
        public static string GetClaimValueFromType(string claimType)
        {
            ClaimsIdentity identity = _context.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                Claim claimData = identity.Claims.FirstOrDefault(x => x.Type.ToLower() == claimType.ToLower());
                if (claimData != null)
                {
                    return claimData.Value;
                }
            }
            return null;
        }
        /*
        public static void AddUpdateClaim(this IPrincipal currentPrincipal, string key, string value)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });
        }
        */
    }
}
