using AllShow.Models.Identity;
using AllShowService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjAllShow.WebAPI.Infrastructure;
using prjAllShow.WebAPI.Models;

namespace prjAllShow.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GetAuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IApplicationUserService _service;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;
        public GetAuthController(IConfiguration config,IApplicationUserService service, ITokenService tokenService)
        {
            _config = config;
            _service = service;
            _tokenService = tokenService;
        }       

        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var user = _service.Authentication(userCredential.UserEmail, userCredential.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                string[] roleNames = _service.GetUserRoles(user.Id).Select(m => m.Role).ToArray();
                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), user, roleNames);

                if (generatedToken != null)
                {
                    return Ok(generatedToken);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        
    }
}
