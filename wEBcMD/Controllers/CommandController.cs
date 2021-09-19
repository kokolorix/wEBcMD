using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace wEBcMD.Controllers
{
   /// <summary>
   /// Controller around the commands
   /// <list type="table">
   ///     <listheader>
   ///         <term>The possible Commands are:</term>
   ///         <description>description</description>
   ///     </listheader>
   ///     <item>
   ///         <term>SampleCommand</term>
   ///         <description>Sample to show how dispatch and execute a command.
   ///         <seealso cref="SampleCommand.ExecuteCommand()"/>
   ///         </description>
   ///     </item>
   /// </list>   /// </summary>
   [ApiController]
   [Route("[controller]")]
   public class CommandController : ControllerBase
   {
      private readonly ILogger<CommandController> _logger;
      /// <summary>
      /// Initialize the logger
      /// </summary>
      /// <param name="logger"></param>
      public CommandController(ILogger<CommandController> logger) => _logger = logger;

      /// <summary>
      /// Resolve and execute the given command
      /// </summary>
      /// <param name="cmd"></param>
      /// <returns>the commands response</returns>
      /// <remarks>
      /// Sample request for <see cref="SampleCommand.ExecuteCommand()"/>:
      ///
      ///      {
      ///        "id": "ee72eaab-d696-46e6-ab41-56f499579be7",
      ///        "type": "e3e185bd-5237-4574-977f-a040bbe12d35",
      ///        "response": false,
      ///        "arguments": [
      ///          {
      ///            "name": "FirstOne",
      ///            "value": "The first argument"
      ///          },
      ///          {
      ///            "name": "SecondOne",
      ///            "value": "true"
      ///          }
      ///        ]
      ///      }
      ///
      /// </remarks>
      [HttpPost]
      [Route("execute")]
      public CommandDTO ExecuteCommand(CommandDTO cmd)
      {
         Log.Trace(_logger, $"Command of type '{cmd.Type}' was called");
         CommandDTO result;

         //******** THIS IS GENERATED CODE. DO NOT CHANGE THIS SECTION ********//

         result = AdressTypesDispatcher.Dispatch(cmd);
         if(null != result)
            return result;

         result = BaseTypesDispatcher.Dispatch(cmd);
         if(null != result)
            return result;

         result = CommandTypesDispatcher.Dispatch(cmd);
         if(null != result)
            return result;

         //******** NEW DISPATCHERS INSERTED HERE                      ********//
         //******** THIS IS GENERATED CODE. DO NOT CHANGE THIS SECTION ********//

         throw new NotImplementedException();
      }
   }
}
