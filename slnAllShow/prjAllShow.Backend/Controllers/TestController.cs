using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.Backend.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestModal()
        {
            return View();
        }

        public IActionResult Index_Popup(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
