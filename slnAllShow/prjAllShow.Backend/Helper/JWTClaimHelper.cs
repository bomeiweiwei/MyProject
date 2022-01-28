using System.Security.Claims;

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
    }
}
