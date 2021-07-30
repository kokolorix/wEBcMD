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
      ///            "FirstOne": "The first argument",
      ///            "SecondOne": true
      ///          }
      ///        ]
      ///      }
      ///
      /// Sample request for <see cref="SampleCommand2.ExecuteCommand()"/>:
      ///
      ///      {
      ///        "id": "ee72eaab-d696-46e6-ab41-56f499579be7",
      ///        "type": "cb0ab4ab-9aeb-4b42-ac2d-152a4555370a",
      ///        "response": false,
      ///        "arguments": [
      ///          {
      ///            "FirstOne": "The first argument",
      ///            "SecondOne": true
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
         if(SampleCommand.IsForMe(cmd))
            return SampleCommand.ExecuteCommand(cmd);
         else if(SampleCommand2.IsForMe(cmd))
            return SampleCommand2.ExecuteCommand(cmd);
         // cmd.Arguments.Add(new PropertyDTO{Name="B", Value="b value"});
         return cmd;
         // throw new NotImplementedException();
      }
   }
}
