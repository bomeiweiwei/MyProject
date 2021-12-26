using IdentityModel;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using AllShow.Models.ViewModels;
using prjAllShow.Backend.Resources;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using AllShow.Models.Identity;
using AllShow.Extensions;

namespace prjAllShow.Backend.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ILogger<HomeController> logger, IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var uIdentity = User.Identity;
            if (uIdentity != null)
            {
                var identity = (ClaimsIdentity)uIdentity;
                IEnumerable<Claim> claims = identity.Claims;

                //設定由AdditionalUserClaimsPrincipalFactory設定，IsAdmin預設Value=admin，其他=factory
                var checkClaim = claims.Where(m => m.Type == JwtClaimTypes.Role).FirstOrDefault();
                if (checkClaim != null)
                {
                    string area = checkClaim.Value;
                    if (area == "admin" || area == "factory")
                    {
                        var userId = User.GetLoggedInUserId<int>();
                        var user = await _userManager.FindByIdAsync(Convert.ToString(userId));
                        //取得登入者所有的role name
                        var roleNameList = await _userManager.GetRolesAsync(user);
                        //將登入者所有的role name全部轉小寫
                        List<string> userRoles = new List<string>();
                        //userRoles.ForEach(role => role.ToUpper());
                        foreach (var r in roleNameList)
                        {
                            userRoles.Add(r.ToLowerInvariant());
                        }
                        //role name全部轉小寫是否有包含area的小寫字串，有則帶進area/Home/Index，沒有則帶進Home/Welcome
                        if (userRoles.Any(m => m == area.ToLowerInvariant()))
                            return RedirectToAction("Index", "Home", new { Area = area });
                        else
                            return RedirectToAction("Welcome", "Home");
                    }

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
