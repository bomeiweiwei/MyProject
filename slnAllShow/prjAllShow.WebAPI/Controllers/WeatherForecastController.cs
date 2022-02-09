using AllShowDTO.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly List<WeatherForecast> weathers = new List<WeatherForecast>
        {
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Freezing"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Bracing"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Chilly"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Cool"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Mild"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Warm"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Balmy"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Hot"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Sweltering"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(1),TemperatureC=10,Summary="Scorching"},

            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Freezing1"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Bracing1"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Chilly1"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Cool1"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Mild1"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Warm1"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Balmy1"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Hot1"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Sweltering1"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(2),TemperatureC=20,Summary="Scorching1"},

            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Freezing2"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Bracing2"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Chilly2"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Cool2"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Mild2"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Warm2"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Balmy2"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Hot2"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Sweltering2"},
            new WeatherForecast{ Date =DateTime.Now.AddDays(3),TemperatureC=30,Summary="Scorching2"},
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        //[Authorize]
        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get(int? page = 1)
        {
            //var list = Enumerable.Range(1, 500).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToList();
            int pageIndex = page.Value - 1;
            int pageSize = 10;
            var list = weathers.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            var response = new ApiReponse<List<WeatherForecast>>(list);

            response.TotalDataCount = weathers.Count();
            response.TotalPageCount = (int)Math.Ceiling((decimal)response.TotalDataCount / 10);

            return Ok(response);
        }
    }
}