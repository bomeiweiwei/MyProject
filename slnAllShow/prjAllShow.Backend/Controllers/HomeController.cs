using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.Backend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
