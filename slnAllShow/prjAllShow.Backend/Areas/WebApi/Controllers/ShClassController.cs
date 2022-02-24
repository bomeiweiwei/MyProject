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
    public class ShClassController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string apiUrl;
        public ShClassController(IConfiguration config)
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
                    url = apiUrl + $"/ShClass/GetByPage/{page}";
                else
                    url = apiUrl + $"/ShClass/GetByPage/1";
            }
            else
            {
                url = apiUrl + $"/ShClass/GetAll";
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
                var apiRes = JsonConvert.DeserializeObject<ApiReponse<List<ShClass>>>(content);
                List<ShClass> res = apiRes.ResultData;
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
            string url = apiUrl + "/ShClass/GetById/" + Id;
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
                var apiRes = JsonConvert.DeserializeObject<ApiReponse<ShClass>>(content);
                ShClass res = apiRes.ResultData;
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
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShClassDTO sendObject)
        {
            string url = apiUrl + "/ShClass/createshclass";

            var client = new HttpClient();

            if (!Request.Cookies.TryGetValue("AccessToken", out string token))
            {
                token = "null";
            }

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(sendObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync(url, data);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            { 
                return Ok();
            }                
            else
                return BadRequest();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ShClassDTO sendObject)
        {
            string url = apiUrl + "/ShClass/updateshclass";

            var client = new HttpClient();

            if (!Request.Cookies.TryGetValue("AccessToken", out string token))
            {
                token = "null";
            }
            sendObject.Id = id;
            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(sendObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsync(url, data);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                return Ok();
            }
            else
                return BadRequest();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string url = apiUrl + "/ShClass/deleteshclass/"+ id;

            var client = new HttpClient();

            if (!Request.Cookies.TryGetValue("AccessToken", out string token))
            {
                token = "null";
            }

            var sendObject = new ShClassDTO() { Id = id };
            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(sendObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                return Ok();
            }
            else
                return BadRequest();
        }
    }   
}
