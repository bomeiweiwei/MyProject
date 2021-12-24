using AllShow.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Models.Identity
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
