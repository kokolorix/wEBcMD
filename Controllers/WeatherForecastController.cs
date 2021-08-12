using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wEBcMD.Controllers
{
   /// <summary>
   /// Sample Controller
   /// </summary>
   [ApiController]
   [Route("[controller]")]
   public class WeatherForecastController : ControllerBase
   {
      private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

      private readonly ILogger<WeatherForecastController> _logger;

      /// <summary>
      ///
      /// </summary>
      /// <param name="logger"></param>
      public WeatherForecastController(ILogger<WeatherForecastController> logger)
      {
         _logger = logger;
      }

      /// <summary>
      ///
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public IEnumerable<WeatherForecast> Get()
      {
         _logger.Log(LogLevel.Information, "Start processing");
         var rng = new Random();
         var res = Enumerable.Range(1, 5).Select(index => new WeatherForecast
         {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
         })
         .ToArray();
          _logger.Log(LogLevel.Information, "End processing");
          return res;
      }
   }
}
