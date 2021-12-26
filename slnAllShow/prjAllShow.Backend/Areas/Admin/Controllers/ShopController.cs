using AllShow.Data;
using AllShow.Models;
using AllShow.Models.Identity;
using AllShowDTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace prjAllShow.Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ShopController : Controller
    {
        private readonly IdentityDBContext _dbContext;
        private readonly AllShowDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ShopController(
            IdentityDBContext dbContext, 
            AllShowDBContext context, 
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _context = context;
            _userManager = userManager;

            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var user = await (from item in _dbContext.Users select item).ToListAsync();
            var shop = await (
                        from item1 in _context.ShopSetting
                        join item2 in _context.EmployeeSetting on item1.EmpNo equals item2.Id
                        select new ShopSettingDTO
                        {
                            Id = item1.Id,
                            EmpNo = item2.Id,
                            EmpName = item2.EmpName,
                            ShAccount = item1.ShAccount,
                            ShLogoPic = item1.ShLogoPic,
                            ShName = item1.ShName,
                            ShBoss = item1.ShBoss,
                            ShContact = item1.ShContact,
                            ShTel = item1.ShTel,
                            ShPopShop = item1.ShPopShop,
                            ShCheckState = item1.ShCheckState,
                            ShPwdState = item1.ShPwdState
                        }).ToListAsync();

            var query = from item2 in user
                        join item1 in shop on item2.Email equals item1.ShAccount
                        select new ShopSettingDTO
                        {
                            AuserId = item2.Id,
                            Id = item1.Id,
                            EmpNo = item1.Id,
                            EmpName = item1.EmpName,
                            ShLogoPic = "/Files/ShowImages?id=" + item1.ShLogoPic,
                            ShName = item1.ShName,
                            ShBoss = item1.ShBoss,
                            ShContact = item1.ShContact,
                            ShTel = item1.ShTel,
                            ShPopShop = item1.ShPopShop,
                            ShCheckState = item1.ShCheckState,
                            ShPwdState = item1.ShPwdState
                        };
            return View(query);
        }

        public async Task<IActionResult> Details(int AuserId, int eId)
        {
            var auser = await _userManager.FindByIdAsync(Convert.ToString(AuserId));
            if (auser == null)
            {
                return NotFound();
            }
            var shop = await _context.ShopSetting
                .FirstOrDefaultAsync(m => m.Id == eId);
            if (shop == null)
            {
                return NotFound();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShopSetting, ShopSettingDTO>();
            });
            IMapper mapper = config.CreateMapper();
            ShopSettingDTO viewModel = mapper.Map<ShopSetting, ShopSettingDTO>(shop);
            viewModel.AuserId = auser.Id;
            viewModel.EmpName = auser.UserName;
            return View(viewModel);
        }

        public IActionResult Create()
        {

            var queryUser = from user in _dbContext.Users
                            join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                            join role in _dbContext.Roles on userRole.RoleId equals role.Id
                            where role.Name == "Customer"
                            select new
                            {
                                user.Id,
                                user.UserName
                            };
            IEnumerable<SelectListItem> items =
                                        from value in queryUser
                                        select new SelectListItem
                                        {
                                            Text = value.UserName.ToString(),
                                            Value = value.Id.ToString(),
                                            // Selected = value.Id == selectedMovie,
                                        };

            ViewBag.IsCustomerRole = items;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(int ShopSelected)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(ShopSelected));
            if (user == null)
            {
                return NotFound();
            }
            //取得登入者所有的role name
            var roleNameList = await _userManager.GetRolesAsync(user);
            if (roleNameList.Contains("Customer"))
            {
                string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                string confirmationLink = Url.Action("ConfirmEmail", "Account",
                                            new
                                            {
                                                Area = "",
                                                userid = user.Id,
                                                token = confirmationToken
                                            }, protocol: HttpContext.Request.Scheme);

                string confirmBody = $"Please confirm your account by clicking this link: <a href=\"{confirmationLink}\">點此連結</a>";

                /*模擬*/
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.
                 SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = @"C:\Test";

                client.Send("test@localhost", user.Email,
                       "Confirm your email",
                       confirmBody);
                /*實際*/
                await _emailSender.SendEmailAsync(user.Email, "Confirm your account", confirmBody);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ShowErrorMsg = "不具有升級廠商身分";
                return View();
            }
        }
    }
}
