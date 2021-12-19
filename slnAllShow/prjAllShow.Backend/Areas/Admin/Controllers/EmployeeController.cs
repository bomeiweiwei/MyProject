using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjAllShow.Backend.Data;
using prjAllShow.Backend.Models.ViewModels;

namespace prjAllShow.Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class EmployeeController : Controller
    {
        private readonly IdentityDBContext _dbContext;
        private readonly AllShowDBContext _context;

        public EmployeeController(IdentityDBContext dbContext, AllShowDBContext context)
        {
            _dbContext = dbContext;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await (from item in _dbContext.Users select item).ToListAsync();
            var emp = await (from item in _context.EmployeeSetting select item).ToListAsync();
            
            var query = from item1 in user
                        join item2 in emp on item1.Email equals item2.EmpEmail
                        select new EmployeeViewModel
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

        public async Task<IActionResult> Details(int? AuserId,int? eId)
        {
            if (AuserId == null || eId == null)
            {
                return NotFound();
            }

            var auser = await _dbContext.Users
                .FirstOrDefaultAsync(m => m.Id == AuserId);
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

            EmployeeViewModel viewModel = new EmployeeViewModel()
            { 
                AuserId = auser.Id,
                Id= employee.Id,
            };
            return View(employee);
        }

    }
}
