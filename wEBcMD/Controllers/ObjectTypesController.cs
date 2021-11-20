
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace wEBcMD.Controllers
{
               /// <summary>
   /// 
   /// Types and commands needed for an object service.
   /// On the client side a caching and repository can be built,
   /// so that the object instances cannot occur more than once.
   /// 
   /// </summary>

   [ApiController]
   [Route("[controller]")]
   public class ObjectTypesController : ControllerBase
   {
      private readonly ILogger<ObjectTypesController> _logger;
      /// <summary>
      /// Initialize the logger
      /// </summary>
      public ObjectTypesController(ILogger<ObjectTypesController> logger) => _logger = logger;

      /// <summary>
      /// Retrieval of all object types known in the system
      /// </summary>

      [HttpGet]
      [Route("getobjecttypes")]
      
      public List<ObjectTypeDTO> GetObjectTypes(  )
      {
         GetObjectTypesWrapper wrapper = new();
         

         return wrapper.GetObjectTypes(
			);
      }
   
   }

}
         