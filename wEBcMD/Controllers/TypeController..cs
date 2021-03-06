using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace wEBcMD.Controllers
{
   /// <summary>
   /// Controller for Object-Types
   /// </summary>
   [ApiController]
   [Route("[controller]")]
   public class ObjectTypeController : ControllerBase
   {
      private readonly ILogger<ObjectTypeController> _logger;
      /// <summary>
      /// Initialize the logger
      /// </summary>
      /// <param name="logger"></param>
      public ObjectTypeController(ILogger<ObjectTypeController> logger) => _logger = logger;

      /// <summary>
      /// Get one concrete type
      /// </summary>
      /// <param name="Id"></param>
      /// <returns>The type requested, or null if not exist</returns>
      [HttpGet]
      [Route("{Id:Guid}")]
      public TypeDTO GetObjectType(Guid Id)
      {
         Log.Trace(_logger);
         TypeDTO type = new()
         {
            Id = Id,
            Type = Guid.Parse("38b38794-0a2e-466c-b198-c831708298f6"),
         };
         return type;
      }

      /// <summary>
      ///  Get all object types
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public IEnumerable<TypeDTO> GetObjectTypes()
      {
         Log.Trace(_logger);
         TypeDTO[] types =
         {
                new TypeDTO {
                    Id=Guid.NewGuid(), Type=Guid.Parse("38b38794-0a2e-466c-b198-c831708298f6"),
                    }
            };
         return types;
      }
   }
}
