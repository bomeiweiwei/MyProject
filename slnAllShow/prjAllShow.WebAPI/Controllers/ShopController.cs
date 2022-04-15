using AllShow.Models;
using AllShowDTO;
using AllShowDTO.Infrastructure;
using AllShowService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IShopService _service;

        public ShopController(ILogger<ShopController> logger, IShopService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize]
        [HttpGet("GetByPage/{page}")]
        public IActionResult GetByPage(int page = 1)
        {
            int pageIndex = page - 1;
            int pageSize = 10;
            var list = _service.GetShopsByPage(pageIndex, pageSize);

            var response = new ApiReponse<List<ShopSettingDTO>>(list);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetByShclass/{Id}")]
        public IActionResult GetByShclass(int Id = 1)
        {
            List<ShopSettingDTO> list = _service.GetShopsByClass(Id).ToList();
            
            list = list.Select(x =>
                 new ShopSettingDTO
                 {
                     Id = x.Id,
                     ShName = x.ShName,
                     ShThePic = x.ShThePic,
                     AuserId = x.AuserId
                 }).ToList();
            var response = new ApiReponse<List<ShopSettingDTO>>(list);
            return Ok(response);
        }
    }
}
