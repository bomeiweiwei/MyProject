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
    public class ProductClassController : ControllerBase
    {
        private readonly ILogger<ProductClassController> _logger;
        private readonly IProductClassService _service;

        public ProductClassController(ILogger<ProductClassController> logger, IProductClassService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var list = _service.GetProductClass().ToList();
            var response = new ApiReponse<List<ProductClass>>(list);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetByPage/{page}")]
        //[HttpGet(Name = "GetShClass")]
        public IActionResult GetByPage(int page = 1)
        {
            int pageIndex = page - 1;
            int pageSize = 10;
            var totalData = _service.GetProductClass();
            var list = totalData.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            var response = new ApiReponse<List<ProductClass>>(list);

            response.TotalDataCount = totalData.Count();
            response.TotalPageCount = (int)Math.Ceiling((decimal)response.TotalDataCount / 10);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id = 1)
        {
            var obj = _service.GetProductClassId(Id);
            var response = new ApiReponse<ProductClass>(obj);
            return Ok(response);
        }
    }
}
