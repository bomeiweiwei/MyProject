using AllShow.Data;
using AllShow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ShClassController : Controller
    {
        private readonly AllShowDBContext _context;
        public ShClassController(AllShowDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            var model = _context.ShClass.Where(m => m.Id == Id).FirstOrDefault();
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
