using AllShow.Models;
using AllShowDTO;
using AllShowDTO.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace prjAllShow.Backend.Areas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductClassController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string apiUrl;
        public ProductClassController(IConfiguration config)
        {
            _config = config;
            this.apiUrl = _config.GetSection("WebAPIUrl").Value;
        }

        [Authorize]
        [HttpGet("GetByPage/{page}")]
        public async Task<IActionResult> GetByPage(string page)
        {
            string url = "";
            if (page != "all")
            {
                int number;
                bool success = Int32.TryParse(page, out number);
                if (success)
                    url = apiUrl + $"/ProductClass/GetByPage/{page}";
                else
                    url = apiUrl + $"/ProductClass/GetByPage/1";
            }
            else
            {
                url = apiUrl + $"/ProductClass/GetAll";
            }

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
                var apiRes = JsonConvert.DeserializeObject<ApiReponse<List<ProductClass>>>(content);
                List<ProductClass> res = apiRes.ResultData;
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
            {
                return BadRequest();
            }

        }
        [Authorize]
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            string url = apiUrl + "/ProductClass/GetById/" + Id;
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
                var apiRes = JsonConvert.DeserializeObject<ApiReponse<ProductClass>>(content);
                ProductClass res = apiRes.ResultData;
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
            {
                return BadRequest();
            }
        }
    }
}
