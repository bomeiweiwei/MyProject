using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AllShow.Data;
using AllShow.Models;
using AllShow.Models.Identity;
using AllShow.Models.ViewModels;
using System.Transactions;
using AllShowDTO;

namespace prjAllShow.Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class EmployeeController : Controller
    {
        private readonly IdentityDBContext _dbContext;
        private readonly AllShowDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeController(IdentityDBContext dbContext, AllShowDBContext context, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await (from item in _dbContext.Users select item).ToListAsync();
            var emp = await (from item in _context.EmployeeSetting select item).ToListAsync();
            
            var query = from item1 in user
                        join item2 in emp on item1.Email equals item2.EmpEmail
                        select new EmployeeSettingDTO
                        {
                            AuserId = item1.Id,
                            Id = item2.Id,
                            EmpName = item2.EmpName,
                            EmpSex = (item2.EmpSex ?? "0"),
                            EmpBirth = item2.EmpBirth,
                            EmpTel = item2.EmpTel,
                            HireDate = item2.HireDate,
                            LeaveDate = item2.LeaveDate,
                            EmpAccountState = (item2.EmpAccountState ?? "0")
                        };
            return View(query);
        }

        public async Task<IActionResult> Details(int AuserId,int eId)
        {
            var auser = await _userManager.FindByIdAsync(Convert.ToString(AuserId));
            if (auser == null)
            {
                return NotFound();
            }
            var employee = await _context.EmployeeSetting
                .FirstOrDefaultAsync(m => m.Id == eId);
            if (employee == null)
            {
                return NotFound();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeSetting, EmployeeSettingDTO>();
            });
            IMapper mapper = config.CreateMapper();
            EmployeeSettingDTO viewModel = mapper.Map<EmployeeSetting, EmployeeSettingDTO>(employee);
            viewModel.AuserId = auser.Id;
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(EmployeeSettingDTO model)
        {
            ModelState.Remove("Id");
            ModelState.Remove("EmpAccount");
            //ModelState.Remove("Authorities");
            //ModelState.Remove("Advertisement");
            //ModelState.Remove("Announcement");
            //ModelState.Remove("Shop");
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var user = new ApplicationUser
                {
                    UserName = model.EmpName,
                    Email = model.EmpEmail,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    IsAdmin = true,
                    CreatedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now
                };
                var hashedPassword = passwordHasher.HashPassword(user, model.EmpPwd);
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
                            var roleName = "Admin";
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
                            cfg.CreateMap<EmployeeSettingDTO, EmployeeSetting>();
                        });
                        IMapper mapper = config.CreateMapper();
                        EmployeeSetting sModel = mapper.Map<EmployeeSettingDTO, EmployeeSetting>(model);

                        sModel.EmpAccount = model.EmpEmail;
                        _context.EmployeeSetting.Add(sModel);
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

        public async Task<IActionResult> Edit(int AuserId, int eId)
        {
            var auser = await _userManager.FindByIdAsync(Convert.ToString(AuserId));
            if (auser == null)
            {
                return NotFound();
            }
            var employee = await _context.EmployeeSetting
                .FirstOrDefaultAsync(m => m.Id == eId);
            if (employee == null)
            {
                return NotFound();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeSetting, EmployeeSettingDTO>();
            });
            IMapper mapper = config.CreateMapper();
            EmployeeSettingDTO viewModel = mapper.Map<EmployeeSetting, EmployeeSettingDTO>(employee);
            viewModel.AuserId = auser.Id;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(EmployeeSettingDTO model)
        {
            //ModelState.Remove("Authorities");
            //ModelState.Remove("Advertisement");
            //ModelState.Remove("Announcement");
            //ModelState.Remove("Shop");
            if (!(model.ChangePwd.HasValue && model.ChangePwd.Value))
            {
                ModelState.Remove("EmpPwd");
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Convert.ToString(model.AuserId));
                if (user != null)
                {
                    var passwordHasher = new PasswordHasher<ApplicationUser>();

                    user.UserName = model.EmpName;
                    user.PhoneNumber = model.EmpTel;
                    user.UpdatedDateTime = DateTime.Now;

                    if (model.ChangePwd.HasValue && model.ChangePwd.Value)
                    {       
                        var hashedPassword = passwordHasher.HashPassword(user, model.EmpPwd);               
                        user.PasswordHash = hashedPassword;
                        model.EmpPwd = hashedPassword;
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
                                    cfg.CreateMap<EmployeeSettingDTO, EmployeeSetting>();
                                }
                                else
                                {
                                    cfg.CreateMap<EmployeeSettingDTO, EmployeeSetting>().ForMember(x => x.EmpPwd, opt => opt.Ignore());
                                }
                            });
                            IMapper mapper = config.CreateMapper();
                            var employee = await _context.EmployeeSetting.FirstOrDefaultAsync(m => m.Id == model.Id);
                            if (employee != null)
                            {
                                employee = mapper.Map(model, employee);
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

        public async Task<IActionResult> DeleteAsync(int AuserId, int eId)
        {
            bool deleteFlag = true;
            var auser = await _userManager.FindByIdAsync(Convert.ToString(AuserId));
            if (auser == null)
            {
                deleteFlag = false;
                return NotFound();
            }

            var employee = await _context.EmployeeSetting
                .FirstOrDefaultAsync(m => m.Id == eId);
            if (employee == null)
            {
                deleteFlag = false;
                return NotFound();
            }

            if (deleteFlag)
            {
                _dbContext.Remove(auser);
                _dbContext.SaveChanges();
                _context.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
