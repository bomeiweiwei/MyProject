using prjAllShow.Backend.Extensions;
using System.Security.Claims;

namespace prjAllShow.Backend.Models.Identity
{
    public class BaseUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<int>();//_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
