using IdentityModel;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using prjAllShow.Backend.Models;
using prjAllShow.Backend.Resources;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace prjAllShow.Backend.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        IStringLocalizer<SharedResources> _localizer;
        public HomeController(ILogger<HomeController> logger, IStringLocalizer<SharedResources> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var uIdentity = User.Identity;
            if (uIdentity != null)
            {
                var identity = (ClaimsIdentity)uIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var checkClaim = claims.Where(m => m.Type == JwtClaimTypes.Role).FirstOrDefault();
                if (checkClaim != null)
                {
                    string area = checkClaim.Value;
                    return RedirectToAction("Index", "Home", new { Area = area });
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            string testEncode = HtmlEncoder.Default.Encode(HttpContext.TraceIdentifier);
            _logger.LogWarning("發生錯誤");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(culture))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }
            return LocalRedirect(returnUrl);
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}
