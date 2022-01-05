using AllShow.Models.Identity;
using AllShowDTO;
using AllShowService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjAllShow.WebAPI.Infrastructure;

namespace prjAllShow.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IApplicationUserService _service;
        public TestController(IApplicationUserService service)
        {
            _service = service;
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public ApiReponse<List<UserDTO>> List()
        {
            var list = _service.GetAll();
            return new ApiReponse<List<UserDTO>>(list);
        }
    }
}
