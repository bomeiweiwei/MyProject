using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.Backend.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
