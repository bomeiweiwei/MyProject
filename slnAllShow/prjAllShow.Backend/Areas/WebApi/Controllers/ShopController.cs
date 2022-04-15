using AllShow.Models;
using AllShowDTO;
using AllShowDTO.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace prjAllShow.Backend.Areas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string apiUrl;
        private readonly string shClassOtherId;
        public ShopController(IConfiguration config)
        {
            _config = config;
            this.apiUrl = _config.GetSection("WebAPIUrl").Value;
            this.shClassOtherId = _config.GetSection("shClassOtherId").Value;
        }

        [Authorize]
        [HttpGet("GetByShclass/{shclassno}")]
        public async Task<IActionResult> GetByShclass(int shclassno)
        {
            string url = apiUrl + "/Shop/GetByShclass/"+ shclassno;

            var client = new HttpClient();

            if (!Request.Cookies.TryGetValue("AccessToken", out string token))
            {
                token = "null";
            }

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                var apiRes = JsonConvert.DeserializeObject<ApiReponse<List<ShopSettingDTO>>>(content);
                List<ShopSettingDTO> res = apiRes.ResultData;
                if (res != null)
                {
                    return Ok(apiRes);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
                return BadRequest();
        }
    }
}
