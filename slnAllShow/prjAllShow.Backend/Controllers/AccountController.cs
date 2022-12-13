using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using AllShow.Extensions;
using AllShow.Models.ViewModels;
using AllShow.Models.Identity;
using prjAllShow.Backend.Resources;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Transactions;
using Microsoft.AspNetCore.Identity.UI.Services;
using AllShow.Data;
using AllShow.Models;
using AllShowCommon;

namespace prjAllShow.Backend.Controllers
{
    [ResponseCache(NoStore = true)]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IdentityDBContext _dbContext;
        private readonly AllShowDBContext _context;
        private readonly ILogger<HomeController> _logger;
        IStringLocalizer<SharedResources> _localizer;
        private readonly string aesKey;
        public AccountController(IConfiguration config,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IStringLocalizer<SharedResources> localizer,
            IdentityDBContext dbContext,
            AllShowDBContext context,
            ILogger<HomeController> logger)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _context = context;
            _localizer = localizer;
            _logger = logger;

            this.aesKey = _config.GetSection("AES_Key").Value;
        }
        /// <summary>
        /// 註冊
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 註冊處理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    IsAdmin = false,
                    CreatedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now,
                };
                //var hashedPassword = passwordHasher.HashPassword(user, model.Password);
                //user.PasswordHash = hashedPassword;
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            //角色名稱
                            //var roleName = "Admin";
                            var roleName = "Customer";
                            //判斷角色是否存在
                            var _result = await _roleManager.RoleExistsAsync(roleName);
                            if (!_result)
                            {
                                var role = new ApplicationRole(roleName);
                                await _roleManager.CreateAsync(role);
                            }
                            //將使用者加入該角色
                            await _userManager.AddToRoleAsync(user, roleName);

                            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            //var callbackUrl = Url.Action(
                            //   "ConfirmEmail", "Account",
                            //   new { userId = user.Id, code = code },
                            //   protocol: Request.Url.Scheme);

                            //await _userManager.SendEmailAsync(user.Id,
                            //   "Confirm your account",
                            //   "Please confirm your account by clicking this link: <a href=\""
                            //                                   + callbackUrl + "\">link</a>");
                            // ViewBag.Link = callbackUrl;   // Used only for initial demo.

                            //return View("DisplayEmail");

                            scope.Complete();
                            //return RedirectToAction("Index", "Home", new { area = "Admin" });
                            return RedirectToAction("Welcome", "Home");
                        }
                        else
                        {
                            ViewBag.ShowErrorMsg = "使用者新增失敗";
                        }
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                    }
                }
            }
            return View(model);
        }
        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登入處理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    //將密碼加密帶到Home Index
                    string aesPWD = AESUtility.AESEncryptor(model.Password, aesKey);
                    return RedirectToAction("Index", "Home", new { pwd = aesPWD });
                }
                else
                {
                    ViewBag.ShowErrorMsg = _localizer["EmailPWDError"];
                    return View(model);
                }
            }
            else
            {
                ViewBag.ShowErrorMsg = _localizer["EmailPWDError"];
                return View();
            }
        }
        /// <summary>
        /// 登出處理
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// 詳細
        /// 資料取得使用 User.FindFirstValue
        ///     Id 用 User.FindFirstValue 取 Id
        ///     UserName 用 User.FindFirstValue 取 UserName
        ///     PhoneNumber 用 _userManager.FindByIdAsync 取 PhoneNumber，User.FindFirstValue好像沒有PhoneNumber
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Details()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userName = User.FindFirstValue(ClaimTypes.Name);
            ViewBag.UserName = HtmlEncoder.Default.Encode(userName);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            ViewBag.UserEmail = HtmlEncoder.Default.Encode(userEmail);
            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));
            ViewBag.PhoneNumber = HtmlEncoder.Default.Encode(user.PhoneNumber ?? "");

            return View();
        }
        /// <summary>
        /// 編輯
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var userId = User.GetLoggedInUserId<int>();

            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));
            EditUserViewModel eUser = new EditUserViewModel() { UserName = user.UserName, Email = user.Email, PhoneNumber = user.PhoneNumber };
            return View(eUser);
        }
        /// <summary>
        /// 編輯處理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!(model.ChangePwd.HasValue && model.ChangePwd.Value))
            {
                ModelState.Remove("Password");
            }
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();

                var userId = User.GetLoggedInUserId<int>();
                var user = await _userManager.FindByIdAsync(Convert.ToString(userId));
                user.UserName = model.UserName;
                user.PhoneNumber= model.PhoneNumber;
                user.UpdatedDateTime = DateTime.Now;
                if (model.ChangePwd.HasValue && model.ChangePwd.Value)
                {
                    var hashedPassword = passwordHasher.HashPassword(user, model.Password);
                    user.PasswordHash = hashedPassword;
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ShowErrorMsg = "更新失敗";
                }
            }
            return View(model);
        }
        /// <summary>
        /// 拒絕採訪
        /// </summary>
        /// <returns></returns>
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {            
            if (userid == null || token == null)
            {
                ViewData["Message"] = "ConfirmEmail failed";
                return RedirectToAction("Error", "Home");
            }
            var user = await _userManager.FindByIdAsync(userid);
            IdentityResult result;
            try
            {
                if (!user.EmailConfirmed)
                {
                    result = await _userManager.ConfirmEmailAsync(user, token);
                }
                else
                {
                    ViewData["Message"] = "Email already Confirm,U can use shop function";
                    //ViewBag.ShowMsg = "Email Confirm,U can use shop function";
                    return View();
                }
            }
            catch (InvalidOperationException ioe)
            {
                // ConfirmEmailAsync throws when the userId is not found.
                ViewData["Message"] = ioe.Message;
                return RedirectToAction("Error", "Home");
            }

            if (result.Succeeded)
            {
                var hashedPassword = user.PasswordHash;
                try
                {
                    //取得登入者所有的role name
                    var roleNameList = await _userManager.GetRolesAsync(user);
                    if (!roleNameList.Contains("Factory"))
                    {
                        IdentityUserRole<int> ur = new IdentityUserRole<int>
                        {
                            RoleId = 3,
                            UserId = Convert.ToInt32(userid),
                        };

                        _dbContext.UserRoles.Add(ur);
                        _dbContext.SaveChanges();
                    }

                    ShopSetting shop = new ShopSetting
                    {
                        EmpNo = 1,
                        ShName = "ShopName",
                        ShAccount = user.Email,
                        ShPwd = hashedPassword,
                        ShBoss = "Boss",
                        ShContact = "Contact",
                        ShAddress = "Address",
                        ShTel = "00-0000000",
                        ShEmail = user.Email,
                        ShAbout = "About",
                        ShAdState = "0",
                        ShPopShop = "1",
                        ShCheckState = "1",
                        ShPwdState = "1",
                        ShStartDate = DateTime.Now,
                        ShEndDate = DateTime.Now.AddYears(1),
                        ShCheckDate = DateTime.Now,
                        ShThePic = "0",
                        ShLogoPic = "0",
                        ShUrl = "",
                        ShAdTitle = "",
                        ShAdPic = "0",
                    };
                    _context.ShopSetting.Add(shop);
                    _context.SaveChanges();

                    int id = shop.Id;

                    var shclasslist = new ShClassList
                    {
                        ShClassNo = 6,
                        ShNo = id,
                        Note = ""
                    };
                    _context.ShClassList.Add(shclasslist);
                    _context.SaveChanges();
                    
                    ViewData["Message"] = "Email Confirm,U can use shop function";
                    return View();
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = "ConfirmEmail failed";
                    return RedirectToAction("Error", "Home");
                }
            }

            ViewData["Message"] = "ConfirmEmail failed";
            return RedirectToAction("Error", "Home");
        }
    }
}
