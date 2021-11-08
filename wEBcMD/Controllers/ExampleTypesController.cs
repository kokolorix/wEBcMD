
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace wEBcMD.Controllers
{
	/// <summary>
	/// Detailed Description of the Command-Module. Meaning and purpose, intentions and limitations.
	/// </summary>

	[ApiController]
	[Route("[controller]")]
	public class ExampleTypesController : ControllerBase
	{
		private readonly ILogger<ExampleTypesController> _logger;
		/// <summary>
		/// Initialize the logger
		/// </summary>
		public ExampleTypesController(ILogger<ExampleTypesController> logger) => _logger = logger;

		/// <summary>
		/// The very first command we designed
		/// </summary>

		[HttpGet]
		[Route("example")]

		public ExampleDTO Example(Guid id)
		{
			ExampleWrapper wrapper = new();

			wrapper.Id = id;

			return wrapper.Example(
					wrapper.Id
			);
		}

	}

}
