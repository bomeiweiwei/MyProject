using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.Backend.Areas.Factory.Controllers
{
    [Area("Factory")]
    //[Authorize(Roles = "Factory")]
    [Authorize(Policy = "RequireFactoryRole")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
