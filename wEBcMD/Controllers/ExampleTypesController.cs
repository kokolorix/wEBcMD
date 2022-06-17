
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
      
      public ExampleDTO Example( Guid id )
      {
         ExampleWrapper wrapper = new();
         
         wrapper.Id = id;

         return wrapper.Example(
					wrapper.Id
			);
      }

		public record ValueDTO
		{
			public static ValueDTO Create(Int32 v) => new ValueDTO.NumberValueDTO.Int32ValueDTO(v);
			public static ValueDTO Create(Int64 v) => new ValueDTO.NumberValueDTO.Int64ValueDTO(v);
			public static ValueDTO Create(Double v) => new ValueDTO.NumberValueDTO.FloatValueDTO(v);
			public static ValueDTO Create(String v) => new ValueDTO.NumberValueDTO.StringValueDTO(v);
			public static ValueDTO Create(Boolean v) => new ValueDTO.NumberValueDTO.BooleanValueDTO(v);
			public static ValueDTO Create(BaseDTO v) => new ValueDTO.NumberValueDTO.ObjectValueDTO(v);

			public Int32? Int32Value
			{
				get
				{
					switch (this)
					{
						case NumberValueDTO.Int32ValueDTO:
							return ((NumberValueDTO.Int32ValueDTO)this).Value;

						case NumberValueDTO.Int64ValueDTO:
							return (Int32)((NumberValueDTO.Int64ValueDTO)this).Value;

						case NumberValueDTO.FloatValueDTO:
							return (Int32)((NumberValueDTO.FloatValueDTO)this).Value;

						case NumberValueDTO.BooleanValueDTO:
							return ((NumberValueDTO.BooleanValueDTO)this).Value ? 1 : 0;

						case NumberValueDTO.StringValueDTO:
							{
								String val = ((NumberValueDTO.StringValueDTO)this).Value;
								if(Int32.TryParse(val, out Int32 i))
									return i;
								return null;
							}

						//case NumberValueDTO.ObjectValueDTO:
						//	return ((NumberValueDTO.ObjectValueDTO)this).Value.ToString();

						default:
							return null;
					}
				}
			}

			public override string ToString()
			{
					switch (this)
					{
						case NumberValueDTO.Int32ValueDTO:
							return ((NumberValueDTO.Int32ValueDTO)this).Value.ToString();

						case NumberValueDTO.Int64ValueDTO:
							return ((NumberValueDTO.Int64ValueDTO)this).Value.ToString();

						case NumberValueDTO.FloatValueDTO:
							return ((NumberValueDTO.FloatValueDTO)this).Value.ToString();

						case NumberValueDTO.BooleanValueDTO:
							return ((NumberValueDTO.BooleanValueDTO)this).Value.ToString();

						case NumberValueDTO.StringValueDTO:
							return ((NumberValueDTO.StringValueDTO)this).Value.ToString();

						case NumberValueDTO.ObjectValueDTO:
							return ((NumberValueDTO.ObjectValueDTO)this).Value.ToString();

						default:
							return String.Empty;
					}
			}

			public record NumberValueDTO : ValueDTO
			{
				public NumberValueDTO() { }
				public NumberValueDTO(Int64 i) : this() { }

				public record Int32ValueDTO(Int32 Value) : NumberValueDTO;
				public record Int64ValueDTO(Int64 Value) : NumberValueDTO;
				public record FloatValueDTO(Double Value) : NumberValueDTO;
			};
			public record BooleanValueDTO(Boolean Value) : ValueDTO();
			public record StringValueDTO(String Value) : ValueDTO();
			public record ObjectValueDTO(BaseDTO Value) : ValueDTO();
		}

		public class ArgDTO
		{
			public String Name { get; set; }

			public ValueDTO Value { get; set; }

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
						  new AdressDTO(){ Adress1 = "Street1", Name1= "Name1"}
						  ) },
				};
			return args;
		}
	}

}
