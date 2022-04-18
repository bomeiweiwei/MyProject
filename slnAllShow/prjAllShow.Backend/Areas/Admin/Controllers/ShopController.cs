using AllShow;
using AllShow.Data;
using AllShow.Models;
using AllShow.Models.Identity;
using AllShowDTO;
using AllShowDTO.Infrastructure;
using AllShowService.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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
        private readonly IShopService _shopService;
        private readonly IMapper _mapper;

        public ShopController(
            IdentityDBContext dbContext, 
            AllShowDBContext context, 
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IShopService shopService,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _context = context;
            _userManager = userManager;

            _emailSender = emailSender;

            _shopService = shopService;
            _mapper = mapper;
        }

        public IActionResult Index(string currentFilter,
                                                string searchString,
                                                int? pageNumber)
        {
            List<ShopSettingDTO> shop = new List<ShopSettingDTO>();
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            if (!pageNumber.HasValue)
                pageNumber = 1;

            shop = _shopService.GetShopsByPage(pageNumber.Value - 1, 10);

            if (!String.IsNullOrEmpty(searchString))
            {
                shop = shop.Where(s => s.ShName.Contains(searchString)).ToList();
            }
           
            int pageSize = 10;
            var result = PaginatedList<ShopSettingDTO>.Create(shop, pageNumber ?? 1, pageSize);
            return View(result);
        }

        public IActionResult Details(int Id)
        {
            ShopSettingDTO shop = _shopService.GetShopById(Id);
            if (shop == null)
            {
                return NotFound();
            }
            else
                return View(shop);
        }

        public async Task<IActionResult> SimpleDetails(int id)
        {
            var shop = await _context.ShopSetting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shop == null)
            {
                return NotFound();
            }
            ShopSettingDTO viewModel = _mapper.Map<ShopSetting, ShopSettingDTO>(shop);
            return View("_ShopDetailPartial", viewModel);
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

        public async Task<IActionResult> Edit(int sId)
        {
            //var auser = await _userManager.FindByIdAsync(Convert.ToString(AuserId));
            //if (auser == null)
            //{
            //    return NotFound();
            //}
            ApplicationUser auser = null;
            var shop = await _context.ShopSetting
                .FirstOrDefaultAsync(m => m.Id == sId);
            if (shop == null)
            {
                return NotFound();
            }
            else
            {
                auser = await _userManager.FindByEmailAsync(shop.ShAccount);
                if (auser == null)
                {
                    return NotFound();
                }
            }

            SetDDLItems(shop.Id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShopSetting, ShopSettingDTO>();
            });
            IMapper mapper = config.CreateMapper();
            ShopSettingDTO viewModel = mapper.Map<ShopSetting, ShopSettingDTO>(shop);
            viewModel.AuserId = auser.Id;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShopSettingDTO model, IFormFile File_ShLogoPic, IFormFile File_ShThePic, IFormFile File_ShAdPic)
        {
            ModelState.Remove("EmpName");
            ModelState.Remove("ShClassName");
            ModelState.Remove("ShLogoPic");
            ModelState.Remove("ShThePic");
            ModelState.Remove("ShAdPic");
            ModelState.Remove("File_ShLogoPic");
            ModelState.Remove("File_ShThePic");
            ModelState.Remove("File_ShAdPic");
            if (!(model.ChangePwd.HasValue && model.ChangePwd.Value))
            {
                ModelState.Remove("ShPwd");
            }
            if (ModelState.IsValid)
            {
                if (File_ShLogoPic != null)
                {
                    if (File_ShLogoPic.Length > 0)
                    {
                        model.ShLogoPic = SaveImg(File_ShLogoPic).ToString();
                    }
                }
                if (File_ShThePic != null)
                {
                    if (File_ShThePic.Length > 0)
                    {
                        model.ShThePic = SaveImg(File_ShThePic).ToString();
                    }
                }
                if (File_ShAdPic != null)
                {
                    if (File_ShAdPic.Length > 0)
                    {
                        model.ShAdPic = SaveImg(File_ShAdPic).ToString();
                    }
                }
                var user = await _userManager.FindByIdAsync(Convert.ToString(model.AuserId));
                if (user != null)
                {
                    var passwordHasher = new PasswordHasher<ApplicationUser>();

                    user.UserName = model.ShContact;
                    user.PhoneNumber = model.ShTel;
                    user.UpdatedDateTime = DateTime.Now;

                    if (model.ChangePwd.HasValue && model.ChangePwd.Value)
                    {
                        var hashedPassword = passwordHasher.HashPassword(user, model.ShPwd);
                        user.PasswordHash = hashedPassword;
                        model.ShPwd = hashedPassword;
                        user.SecurityStamp = Guid.NewGuid().ToString();
                    }

                    try
                    {
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {                            
                            var config = new MapperConfiguration(cfg =>
                            {
                                if (model.ChangePwd.HasValue && model.ChangePwd.Value)
                                {
                                    cfg.CreateMap<ShopSettingDTO, ShopSetting>();
                                }
                                else
                                {
                                    cfg.CreateMap<ShopSettingDTO, ShopSetting>().ForMember(x => x.ShPwd, opt => opt.Ignore());
                                }
                            });
                            IMapper mapper = config.CreateMapper();
                            var shop = await _context.ShopSetting.FirstOrDefaultAsync(m => m.Id == model.Id);
                            if (shop != null)
                            {
                                //原本已存在的ShClassList
                                var scl = _context.ShClassList.Where(m => m.ShNo == model.Id).ToList();
                                if (model.ShClassListID.Count > 0)
                                {
                                    foreach (var item in model.ShClassListID)
                                    {
                                        int id = Int32.Parse(item);
                                        var check = scl.Where(m => m.ShClassNo == id).FirstOrDefault();
                                        if (check == null)
                                        {
                                            ShClassList shClassList = new ShClassList() { ShClassNo = id, ShNo = shop.Id, Note = "" };
                                            _context.ShClassList.Add(shClassList);
                                            _context.SaveChanges();
                                        }
                                    }
                                    var delScl = scl.Where(m => model.ShClassListID.Any(c => m.ShClassNo.ToString() != c)).ToList();
                                    foreach (var item in delScl)
                                    {
                                        _context.ShClassList.Remove(item);
                                        _context.SaveChanges();
                                    }
                                }

                                shop = mapper.Map(model, shop);
                                _context.SaveChanges();
                                return RedirectToAction("Index");                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                    }
                }
            }
            SetDDLItems(model.Id);
            return View(model);
        }
        public async Task<IActionResult> DeleteAsync(int AuserId, int sId)
        {
            bool deleteFlag = true;
            var auser = await _userManager.FindByIdAsync(Convert.ToString(AuserId));
            if (auser == null)
            {
                deleteFlag = false;
                return NotFound();
            }

            var setting = await _context.ShopSetting
                .FirstOrDefaultAsync(m => m.Id == sId);
            if (setting == null)
            {
                deleteFlag = false;
                return NotFound();
            }

            if (deleteFlag)
            {
                _dbContext.Remove(auser);
                _dbContext.SaveChanges();
                _context.Remove(setting);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 設定核准人員下拉選項
        /// </summary>
        private void SetDDLItems(int shopId)
        {
            IEnumerable<SelectListItem> items =
                                       from value in _context.EmployeeSetting
                                       select new SelectListItem
                                       {
                                           Text = value.EmpName.ToString(),
                                           Value = value.Id.ToString(),
                                            // Selected = value.Id == selectedMovie,
                                        };
            var scitems =
                        (from value in _context.ShClass
                        select new
                        {
                            ShClassName = value.ShClassName.ToString(),
                            Id = value.Id.ToString(),
                            // Selected = value.Id == selectedMovie,
                        }).ToList();
            List<int> scitems_Selected = (
                                    from item1 in _context.ShClassList
                                    join item2 in _context.ShClass on item1.ShClassNo equals item2.Id
                                    join item3 in _context.ShopSetting on item1.ShNo equals item3.Id
                                    where item3.Id == shopId
                                    select item2
                                    ).Select(m => m.Id).ToList();    

            ViewBag.ApproveEmp = items;
            ViewBag.ShClass = new MultiSelectList(scitems, "Id", "ShClassName", scitems_Selected);
        }

        private int SaveImg(IFormFile formFile)
        {
            byte[] buffer = new byte[formFile.Length];
            var resultInBytes = ConvertToBytes(formFile);
            Array.Copy(resultInBytes, buffer, resultInBytes.Length);

            DbFiles dbFiles = new DbFiles()
            {
                Name = Path.GetRandomFileName(),
                MimeType = formFile.ContentType,
                Size = (int)formFile.Length,
                Contnet = buffer
            };
            try
            {
                int newId=SaveDbFiles(dbFiles);
                return newId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private byte[] ConvertToBytes(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.OpenReadStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        
        private int SaveDbFiles(DbFiles dbFiles)
        {
            _context.Add(dbFiles);
            _context.SaveChanges();

            int id = dbFiles.Id;
            return id;
        }
    }
}
