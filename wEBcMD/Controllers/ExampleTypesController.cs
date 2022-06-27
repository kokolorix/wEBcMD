
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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

		/// <summary>
		/// Test for the inherited ValueDTO
		/// </summary>
		/// <returns></returns>
		/// 
		[HttpGet]
		[Route("test")]

		public List<ArgDTO> Test()
		{
			List<ArgDTO> args = new()
			{
				new (){ Name= "Arg1", Value = ValueDTO.Create(7) },
				new (){ Name= "Arg2", Value = ValueDTO.Create(3.14) },
				new (){ Name= "Arg3", Value = ValueDTO.Create(true) },
				new (){ Name= "Arg4", Value = ValueDTO.Create("Holdrio") },
				new (){ Name= "Arg5", Value = ValueDTO.Create(
					new ExampleDTO()
					{
						Id = Guid.NewGuid(),
						One = "1",
						Two= true,
					}
				) },
				new (){ Name= "Arg6", Value = ValueDTO.Create(
					new AdressDTO(){
						Id = Guid.NewGuid(),
						Adress1 = "Street1",
						Name1= "Name1",
					}
				) },
			};
			return args;
		}

		[HttpPost]
		[Route("echo")]
		public IList<ArgDTO> Echo(IList<ArgDTO> args)
		{
			return args;
		}


		[HttpPost]
		[Route("echo-value")]
		public
		ValueDTO Echo(ValueDTO val)
		{
			return val;
		}
	}
}
