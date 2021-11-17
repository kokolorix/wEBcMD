
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace wEBcMD.Controllers
{
               /// <summary>
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
         