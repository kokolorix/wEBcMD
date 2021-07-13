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
		public BaseDTO GetObject(
			Guid Id,
			[System.Runtime.CompilerServices.CallerMemberName] string callerName = ""
		)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public IEnumerable<BaseDTO> GetObjectsRandom(
			[System.Runtime.CompilerServices.CallerMemberName] string callerName = ""
		)
		{
			Trace($"{callerName} called me :-)");
			// System.Diagnostics.Debug.Assert(false, "Holdrio");

			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new BaseDTO
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
