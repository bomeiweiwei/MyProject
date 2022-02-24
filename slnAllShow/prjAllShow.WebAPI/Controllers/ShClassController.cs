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
    public class ShClassController : ControllerBase
    {
        private readonly ILogger<ShClassController> _logger;
        private readonly IShClassService _service;

        public ShClassController(ILogger<ShClassController> logger, IShClassService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var list = _service.GetShClass().ToList();
            var response = new ApiReponse<List<ShClass>>(list);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetByPage/{page}")]
        //[HttpGet(Name = "GetShClass")]
        public IActionResult GetByPage(int page = 1)
        {
            int pageIndex = page - 1;
            int pageSize = 10;
            var totalData = _service.GetShClass();
            var list = totalData.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            var response = new ApiReponse<List<ShClass>>(list);

            response.TotalDataCount = totalData.Count();
            response.TotalPageCount = (int)Math.Ceiling((decimal)response.TotalDataCount / 10);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id = 1)
        {
            var obj = _service.GetShClassId(Id);
            var response = new ApiReponse<ShClass>(obj);
            return Ok(response);
        }
        
        [Authorize]
        [HttpPost("createshclass")]
        public IActionResult Post(ShClassDTO model)
        {
            _service.CreateShClass(new ShClass() { ShClassName = model.ShClassName });
            return Ok();
        }

        [Authorize]
        [HttpPut("updateshclass")]
        public IActionResult Put(ShClassDTO model)
        {
            ShClass shClass = _service.GetShClassId(model.Id);
            shClass.ShClassName = model.ShClassName;
            _service.UpdateShClass(shClass);
            return Ok();
        }

        [Authorize]
        [HttpDelete("deleteshclass/{Id}")]
        public IActionResult Delete(int Id)
        {
            _service.DeleteShClass(Id);
            return Ok();
        }
    }
}
