﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
