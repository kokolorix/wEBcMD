using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace wEBcMD.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class ObjectController : ControllerBase
   {
      private static readonly string[] Names = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

      private readonly ILogger<ObjectController> _logger;
      public ObjectController(ILogger<ObjectController> logger)
      {
         _logger = logger;
      }

      [HttpGet]
      [Route("{Id:Guid}")]
      public ObjectDTO GetObject(Guid Id)
      {
         Log.Trace(_logger);
         throw new NotImplementedException();
      }

      [HttpPost]
      //[Route("")]
      public ObjectDTO GetObject(ObjectDTO obj)
      {
         Log.Trace(_logger);
         throw new NotImplementedException();
      }

      [HttpGet]
      public IEnumerable<ObjectDTO> GetObjectsRandom(
      )
      {
         Log.Trace(_logger);
         // System.Diagnostics.Debug.Assert(false, "Holdrio");

         var rng = new Random();
         return Enumerable.Range(1, 5).Select(index => new ObjectDTO
         {
            Id = Guid.NewGuid(),
            Type = Guid.Parse("1a81bc99-28c2-4c03-ac5c-a1de4967cc36"),
            Properties = Enumerable.Range(1, 5).Select(index => new PropertyDTO
            {
               Name = Names[rng.Next(Names.Length)],
               Value = rng.Next().ToString()
            }).ToList()
         })
         .ToArray();
      }
   }
}
