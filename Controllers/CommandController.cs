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
   public class CommandController : ControllerBase
   {
      private readonly ILogger<CommandController> _logger;
      public CommandController(ILogger<CommandController> logger)
      {
         _logger = logger;
      }

      /// <summary>
      /// Resolve and execute the given command
      /// </summary>
      /// <param name="cmd"></param>
      /// <returns>the commands response</returns>
      [HttpPost]
      [Route("execute")]
      public CommandDTO ExecuteCommand(CommandDTO cmd)
      {
         Log.Trace(_logger, $"{cmd.Arguments.Find(a => a.Name == "A")?.Value}");
         cmd.Response = true;
         cmd.Arguments.Add(new PropertyDTO{Name="B", Value="b value"});
         return cmd;
         // throw new NotImplementedException();
      }
   }
}
