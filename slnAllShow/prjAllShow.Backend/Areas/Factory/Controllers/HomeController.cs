using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.Backend.Areas.Factory.Controllers
{
    [Area("Factory")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
