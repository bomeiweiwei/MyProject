using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class OrderListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
