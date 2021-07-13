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
	public class ObjectTypeController : ControllerBase
	{
		private void Trace(string message,
		[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
		[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
		[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
			System.Diagnostics.Trace.WriteLine(msg);
			_logger.Log(LogLevel.Trace, msg);
			Console.WriteLine(msg);
		}
		private readonly ILogger<ObjectTypeController> _logger;
		public ObjectTypeController(ILogger<ObjectTypeController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<ObjectTypeDTO> GetObjectTypes(
			[System.Runtime.CompilerServices.CallerMemberName] string callerName = ""
		)
		{
			ObjectTypeDTO[] types = 
			{

			};
			return types;
		}
	}
}
