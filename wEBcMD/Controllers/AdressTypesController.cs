
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
   /// Types and Commands around Adresses.
   /// 
   /// </summary>

   [ApiController]
   [Route("[controller]")]
   public class CommandController : ControllerBase
   {
      private readonly ILogger<CommandController> _logger;
      /// <summary>
      /// Initialize the logger
      /// </summary>
      public CommandController(ILogger<CommandController> logger) => _logger = logger;
      /// <summary>
      /// Addresses search, with multiple tokens
      /// </summary>
      /// <summary>
      /// </summary>
      /// <summary>
      /// Updates an existing address if Id is specified,
      /// or creates a new one if Id is null.
      /// The updated or newly created address is returned in Result.
      /// </summary>

   }

}
      