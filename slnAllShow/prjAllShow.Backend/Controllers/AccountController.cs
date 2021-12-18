using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using prjAllShow.Backend.Extensions;
using prjAllShow.Backend.Models;
using prjAllShow.Backend.Models.Identity;
using prjAllShow.Backend.Resources;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Transactions;

namespace prjAllShow.Backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        IStringLocalizer<SharedResources> _localizer;
        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<ApplicationRole> roleManager,
            IStringLocalizer<SharedResources> localizer, 
            ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            _localizer = localizer;
            _logger = logger;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

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
                    IsAdmin = true,
                };
                var hashedPassword = passwordHasher.HashPassword(user, model.Password);
                user.PasswordHash = hashedPassword;
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var result = await _userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            //角色名稱
                            var roleName = "Admin";
                            //判斷角色是否存在
                            var _result = await _roleManager.RoleExistsAsync(roleName);
                            if (!_result)
                            {
                                var role = new ApplicationRole(roleName);
                                await _roleManager.CreateAsync(role);
                            }
                            //將使用者加入該角色
                            await _userManager.AddToRoleAsync(user, roleName);

                            scope.Complete();
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model/*, string returnUrl*/)
        {
            //returnUrl = returnUrl ?? Url.Content("~/");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return RedirectToAction("Index", "Home");//LocalRedirect(returnUrl);
            }
            else
            {
                ViewBag.ShowErrorMsg = _localizer["EmailPWDError"];
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            ViewBag.UserEmail= HtmlEncoder.Default.Encode(userEmail);
            return View();
        }

        public async Task<IActionResult> Edit()
        {
            var userId = User.GetLoggedInUserId<int>(); // Specify the type of your UserId;
            //var userName = User.GetLoggedInUserName();
            var userEmail = User.GetLoggedInUserEmail();

            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));
            EditUserViewModel eUser = new EditUserViewModel() { UserName = user.UserName, Email = userEmail, PhoneNumber = user.PhoneNumber };
            return View(eUser);
        }

        [HttpPost]
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

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
