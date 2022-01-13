using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace wEBcMD
{
	public class JsonConverterExampleDTO:  JsonConverter<ExampleDTO>
	{
		private readonly ILogger<JsonConverterExampleDTO> _logger;
		//JsonConverterExampleDTO(ILogger<JsonConverterExampleDTO> logger)
		//			=> _logger = logger;
		public override bool CanConvert(Type typeToConvert)
		{
			bool res =  typeToConvert == typeof(ExampleDTO)
				|| typeToConvert == typeof(BaseDTO) ;
			//_logger.LogTrace("Can I convert {}? {}", typeToConvert.Name, res);
			return res;
		}

		/// <summary>Read the appropriate structure for typescript-guid</summary>
		public override ExampleDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ExampleDTO example = new() { Id = Guid.NewGuid(), One = "Juhui", Two = true, Type = ExampleDTO.TypeId };
			reader.Read(); // read over start of object
			string res;					//					//string propertyName = reader.GetString();
			reader.Read();
			example.Id = reader.GetGuid();
			reader.Read(); // read over property's name
			res = reader.GetString(); // read over property's name
			reader.Read(); // read over property's name
			example.One = reader.GetString();
			reader.Read();
			res = reader.GetString(); // read over property's name
			reader.Read();
			example.Two = reader.GetBoolean();
			//Guid guid = reader.GetGuid();
			reader.Read(); // read over end of object
			//example.Type = reader.GetGuid();
			//return guid;
			return example;
		}

		/// <summary>Write the appropriate structure for typescript-guid</summary>
		public override void Write(Utf8JsonWriter writer, ExampleDTO value, JsonSerializerOptions options)
		{
			ExampleDTO example = new() { Id = Guid.NewGuid(), One = "Juhui", Two = true, Type = ExampleDTO.TypeId };
			value = example;
			writer.WriteStartObject();
			writer.WriteString("Id", value.Id);
			writer.WriteString("One", value.One);
			writer.WriteBoolean("Two", value.Two);
			writer.WriteString("Type", value.Type);
			writer.WriteEndObject();
		}


/*

	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class JsonConverterGuid : JsonConverter<Guid>
{

		/// <summary>Read the appropriate structure for typescript-guid</summary>
		public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         reader.Read(); // read over start of object
         //string propertyName = reader.GetString();
         reader.Read(); // read over property's name
         Guid guid = reader.GetGuid();
         reader.Read(); // read over end of object
         return guid;
      }

      /// <summary>Write the appropriate structure for typescript-guid</summary>
      public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
      {
         writer.WriteStartObject();
         writer.WriteString("value", value);
         writer.WriteEndObject();
      }
*/
   }
}
