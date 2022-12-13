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
using System.Security.Cryptography;
using AllShowCommon;
using AllShowDTO;
using Newtonsoft.Json;
using System.Text;

namespace prjAllShow.Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<HomeController> _logger;
        IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string apiUrl;
        private readonly string aesKey;
        public HomeController(ILogger<HomeController> logger, IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _logger = logger;
            _localizer = localizer;
            _userManager = userManager;
            _config = config;

            this.apiUrl = _config.GetSection("WebAPIUrl").Value;
            this.aesKey = _config.GetSection("AES_Key").Value;
        }

        public async Task<IActionResult> IndexAsync(string? pwd)
        {
            var uIdentity = User.Identity;
            if (uIdentity != null)
            {
                DateTime loginDt = DateTime.UtcNow;

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
                        //登入的密碼(已用AES加密)
                        if (string.IsNullOrEmpty(pwd))
                        {
                            pwd = "";
                        }

                        //role name全部轉小寫是否有包含area的小寫字串，有則帶進area/Home/Index，沒有則帶進Home/Welcome(因為註冊時是Customer，還沒有正式變成Factory，要去信件點連結)
                        if (userRoles.Contains("superadmin"))
                        {                           
                            await GetAuthTokenAsync(loginDt, pwd);
                            return RedirectToAction("Index", "Home", new { Area = "admin" });
                        }
                        else if (userRoles.Any(m => m == area.ToLowerInvariant()))
                        {
                            await GetAuthTokenAsync(loginDt, pwd);
                            return RedirectToAction("Index", "Home", new { Area = area });
                        }
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

        private async Task GetAuthTokenAsync(DateTime loginDt, string pwd)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));

            string aesEmail = AESUtility.AESEncryptor(userEmail, aesKey);
            string aesPWD = pwd;

            //The data that needs to be sent. Any object works.
            var sendObject = new
            {
                userEmail = aesEmail,
                password = aesPWD
            };

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(sendObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            //The url to post to.
            var url = apiUrl + @"/GetAuth/authentication";
            TokenResult res = null;
            using (var client = new HttpClient())
            {
                //Pass in the full URL and the json string content
                var response = await client.PostAsync(url, data);

                //It would be better to make sure this request actually made it through
                //tokenStr = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<TokenResult>(await response.Content.ReadAsStringAsync());
            }

            if (res != null)
            {
                if (res.Success)
                {
                    Response.Cookies.Append("AccessToken", res.AccessToken, new CookieOptions()
                    {
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        HttpOnly = true
                    });
                    Response.Cookies.Append("RefreshToken", res.RefreshToken, new CookieOptions()
                    {
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        HttpOnly = true
                    });
                    DateTime dt = loginDt.AddSeconds(res.ExpiresIn);
                    string dts = dt.Ticks.ToString();
                    Response.Cookies.Append("Expires", dts, new CookieOptions()
                    {
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        HttpOnly = true
                    });
                }
            }
        }
    }
}
