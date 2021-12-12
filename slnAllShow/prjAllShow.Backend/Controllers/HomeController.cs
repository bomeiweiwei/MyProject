using Microsoft.AspNetCore.Mvc;
using NLog.Web;

namespace prjAllShow.Backend.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //_logger.LogInformation("使用者登入首頁");
            return View();
        }

        public IActionResult Login() 
        {
            //_logger.LogInformation("使用者進入登入頁面");
            //_logger.LogError("使用者登入失敗");
            return View();
        }
    }
}
