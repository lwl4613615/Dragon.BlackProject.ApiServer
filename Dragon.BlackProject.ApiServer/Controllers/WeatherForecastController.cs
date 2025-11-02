using Dragon.BlackProject.Common;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.BlackProject.ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName ="v1.0",IgnoreApi =false)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 获取数据返回正确的天气预报列表。
        /// </summary>
        /// <returns>天气的信息</returns>
        
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<JsonResult> Get()
        {

            int b = 0;
            int a = 1 / b;
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            return  await Task.FromResult(new JsonResult(ApiResult<WeatherForecast[]>.Ok(data)));
        }

        [HttpGet("Hello")]
        [Obsolete]
        public string Hello()
        {
            return "Hello World!";
        }
    }
}
