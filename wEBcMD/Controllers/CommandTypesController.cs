
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace wEBcMD.Controllers
{
               /// <summary></summary>

   [ApiController]
   [Route("[controller]")]
   public class CommandTypesController : ControllerBase
   {
      private readonly ILogger<CommandTypesController> _logger;
      /// <summary>
      /// Initialize the logger
      /// </summary>
      public CommandTypesController(ILogger<CommandTypesController> logger) => _logger = logger;

      /// <summary>
      /// Get a list of all Command-Types
      /// </summary>

      [HttpGet]
      [Route("getcommandtypes")]
      
      public List<CommandTypeDTO> GetCommandTypes(  )
      {
         GetCommandTypesWrapper wrapper = new();
         

         return wrapper.GetCommandTypes(
			);
      }
         /// <summary>
      /// 
      /// This is the sample command. He has two Parameters
      /// and a multiline summary.
      /// ``` typescript
      /// CommandDTO cmd;
      /// if(SampleCommand.IsForMe(dto)){ let sample = new SampleCommand(cmd); console.log(sample.FirstOne);
      /// }
      /// ```
      /// 
      /// </summary>

      [HttpPost]
      [Route("samplecommand")]
      
      public void SampleCommand( String firstOne, Boolean secondOne )
      {
         SampleCommandWrapper wrapper = new();
         
         wrapper.FirstOne = firstOne;
         wrapper.SecondOne = secondOne;

         wrapper.SampleCommand(
					wrapper.FirstOne, 
					wrapper.SecondOne
			);
      }
   
   }

}
         