﻿using AllShowDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace prjAllShow.Backend.Areas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string apiUrl;

        public WeatherController(IConfiguration config)
        {
            _config = config;
            this.apiUrl = _config.GetSection("WebAPIUrl").Value;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetWeatherAsync()
        {
            var url = apiUrl + @"/WeatherForecast";
            var client = new HttpClient();

            if (!Request.Cookies.TryGetValue("AccessToken", out string token))
            {
                token = "null";
            }

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", token);
            //Pass in the full URL and the json string content
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                List<WeatherForecastDTO> res = JsonConvert.DeserializeObject<List<WeatherForecastDTO>>(content);
                if (res != null)
                { 
                    return Ok(res.ToArray()); 
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