using AllShow.Data;
using AllShow.Models;
using AllShow.Models.Identity;
using AllShowDTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace prjAllShow.Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class MemberController : Controller
    {
        private readonly IdentityDBContext _dbContext;
        private readonly AllShowDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberController(IdentityDBContext dbContext, AllShowDBContext context, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await(from item in _dbContext.Users select item).ToListAsync();
            var mem = await(from item in _context.MemberSetting select item).ToListAsync();

            var query = from item1 in user
                        join item2 in mem on item1.Email equals item2.MemEmail
                        select new MemberSettingDTO
                        {
                            AuserId = item1.Id,
                            Id = item2.Id,
                            MemEmail = item2.MemEmail,
                            MemDiminutive = item2.MemDiminutive,
                            MemName = item2.MemName,
                            MemSex = item2.MemSex,
                            MemTel = item2.MemTel,
                            MemAddress = item2.MemAddress,
                            MemPic = item2.MemPic,
                            MemAccountState = item2.MemAccountState,
                            MemCheckNumber = item2.MemCheckNumber,
                            MemBirth = item2.MemBirth,
                            MemCreateDate = item2.MemCreateDate,
                            MemUpdateDate = item2.MemUpdateDate
                        };
            return View(query);
        }

        public async Task<IActionResult> Details(int AuserId, int mId)
        {
            var auser = await _userManager.FindByIdAsync(Convert.ToString(AuserId));
            if (auser == null)
            {
                return NotFound();
            }
            var member = await _context.MemberSetting
                .FirstOrDefaultAsync(m => m.Id == mId);
            if (member == null)
            {
                return NotFound();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MemberSetting, MemberSettingDTO>();
            });
            IMapper mapper = config.CreateMapper();
            MemberSettingDTO viewModel = mapper.Map<MemberSetting, MemberSettingDTO>(member);
            viewModel.AuserId = auser.Id;
            viewModel.MemName = auser.UserName;
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MemberSettingDTO model, IFormFile File_MemPic)
        {
            ModelState.Remove("Id");
            ModelState.Remove("MemCheckNumber");
            ModelState.Remove("MemCreateDate");
            ModelState.Remove("MemPic");
            ModelState.Remove("File_MemPic");
            ModelState.Remove("MemDiminutive");
            if (ModelState.IsValid)
            {
                if (File_MemPic != null)
                {
                    if (File_MemPic.Length > 0)
                    {
                        model.MemPic = SaveImg(File_MemPic).ToString();
                    }
                }
                else
                {
                    model.MemPic = "0";
                }

                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var user = new ApplicationUser
                {
                    UserName = model.MemName,
                    Email = model.MemEmail,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    IsAdmin = false,
                    CreatedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now
                };
                var hashedPassword = passwordHasher.HashPassword(user, model.MemPwd);
                user.PasswordHash = hashedPassword;

                bool aUserCreated = false;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var result = await _userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            //角色名稱
                            var roleName = "Member";
                            //判斷角色是否存在
                            //將使用者加入該角色
                            await _userManager.AddToRoleAsync(user, roleName);

                            scope.Complete();
                            aUserCreated = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        string message = ex.Message;
                    }
                }
                if (aUserCreated)
                {
                    try
                    {
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<MemberSettingDTO, MemberSetting>();
                        });
                        IMapper mapper = config.CreateMapper();
                        MemberSetting sModel = mapper.Map<MemberSettingDTO, MemberSetting>(model);
                        sModel.MemDiminutive = sModel.MemName;
                        sModel.MemCheckNumber = RandomString(5);
                        sModel.MemCreateDate = DateTime.Now;
                        sModel.MemUpdateDate = DateTime.Now;

                        _context.MemberSetting.Add(sModel);
                        int result = _context.SaveChanges();
                        if (result == 1)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int AuserId, int mId)
        {
            var auser = await _userManager.FindByIdAsync(Convert.ToString(AuserId));
            if (auser == null)
            {
                return NotFound();
            }
            var member = await _context.MemberSetting
                .FirstOrDefaultAsync(m => m.Id == mId);
            if (member == null)
            {
                return NotFound();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MemberSetting, MemberSettingDTO>();
            });
            IMapper mapper = config.CreateMapper();
            MemberSettingDTO viewModel = mapper.Map<MemberSetting, MemberSettingDTO>(member);
            viewModel.AuserId = auser.Id;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(MemberSettingDTO model, IFormFile File_MemPic)
        {
            ModelState.Remove("File_MemPic");
            if (!(model.ChangePwd.HasValue && model.ChangePwd.Value))
            {
                ModelState.Remove("MemPwd");
            }
            if (ModelState.IsValid)
            {
                if (File_MemPic != null)
                {
                    if (File_MemPic.Length > 0)
                    {
                        model.MemPic = SaveImg(File_MemPic).ToString();
                    }
                }

                var user = await _userManager.FindByIdAsync(Convert.ToString(model.AuserId));
                if (user != null)
                {
                    var passwordHasher = new PasswordHasher<ApplicationUser>();

                    user.UserName = model.MemName;
                    user.PhoneNumber = model.MemTel;
                    user.UpdatedDateTime = DateTime.Now;

                    if (model.ChangePwd.HasValue && model.ChangePwd.Value)
                    {
                        var hashedPassword = passwordHasher.HashPassword(user, model.MemPwd);
                        user.PasswordHash = hashedPassword;
                        model.MemPwd = hashedPassword;
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
                                    cfg.CreateMap<MemberSettingDTO, MemberSetting>();
                                }
                                else
                                {
                                    cfg.CreateMap<MemberSettingDTO, MemberSetting>().ForMember(x => x.MemPwd, opt => opt.Ignore());
                                }
                            });
                            IMapper mapper = config.CreateMapper();
                            var member = await _context.MemberSetting.FirstOrDefaultAsync(m => m.Id == model.Id);
                            if (member != null)
                            {
                                member = mapper.Map(model, member);
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
            return View(model);
        }

        public async Task<IActionResult> DeleteAsync(int AuserId, int mId)
        {
            bool deleteFlag = true;
            var auser = await _userManager.FindByIdAsync(Convert.ToString(AuserId));
            if (auser == null)
            {
                deleteFlag = false;
                return NotFound();
            }

            var member = await _context.MemberSetting
                .FirstOrDefaultAsync(m => m.Id == mId);
            if (member == null)
            {
                deleteFlag = false;
                return NotFound();
            }

            if (deleteFlag)
            {
                _dbContext.Remove(auser);
                _dbContext.SaveChanges();
                _context.Remove(member);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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
                int newId = SaveDbFiles(dbFiles);
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
