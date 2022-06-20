using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static wEBcMD.ValueDTO;

namespace wEBcMD.Tests
{
	public class JsonConverterValueDTO : JsonConverter<ValueDTO>
	{
		/// <summary>Write the appropriate structure for ValueDTO</summary>
		public override void Write(Utf8JsonWriter writer, ValueDTO value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			switch (value)
			{
				case NumberValueDTO.Int32ValueDTO:
					{
						writer.WritePropertyName("Int32Value");
						writer.WriteNumberValue((decimal)value.Int32Value);
						break;
					}
				case NumberValueDTO.Int64ValueDTO:
					{
						writer.WritePropertyName("Int64Value");
						writer.WriteNumberValue((decimal)value.Int64Value);
						break;
					}
			}

			writer.WriteEndObject();
		}
		/// <summary>Read the appropriate structure for cref="ValueDTO"</summary>
		public override ValueDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ValueDTO value = null;

			reader.Read(); // read over start of object
			string propertyName = reader.GetString();
			reader.Read(); // read over name of property

			switch (propertyName)
			{
				case "Int32Value":
					{
						value = ValueDTO.Create(reader.GetInt32());
						break;
					}
				case "Int64Value":
					{
						value = ValueDTO.Create(reader.GetInt64());
						break;
					}

				default:
					{
						break;
					}
			}

			reader.Read(); // read over property's value

			reader.Read(); // read over end of object
			return value;
		}
	}


	[TestClass]
	public class CommonTests
	{
		void Test<T>(T v, JsonSerializerOptions options)
		{
			ValueDTO val1 = Create1(v);
			String json = JsonSerializer.Serialize(val1, options);
			Console.WriteLine("Serialize: {0}", json);

			ValueDTO val2 = JsonSerializer.Deserialize<ValueDTO>(json, options);	
			json = JsonSerializer.Serialize(val2, options);
			Console.WriteLine("Deserialized: {0}", json);

			Assert.AreEqual(val1, val2);
		}

		[TestMethod]
		public void TestMethod1()
		{
			JsonSerializerOptions options = new JsonSerializerOptions();
			options.AllowTrailingCommas = true;
			options.Converters.Add(new JsonConverterValueDTO());
			 
			Test<Int32>(42, options);
			//Test<Int64>(Int64.MaxValue, options);
			//Test<Double>(3.14, options);


			// https://github.com/dahomey-technologies/Dahomey.Json/blob/master/src/Dahomey.Json.Tests/DiscriminatorTests.cs
			//options.SetupExtensions();
			//DiscriminatorConventionRegistry registry = options.GetDiscriminatorConventionRegistry();
			//registry.RegisterConvention(new AttributeBasedDiscriminatorConvention<string>(options, "Tag"));
			//registry.RegisterType<Box>();
			//registry.RegisterType<Circle>();

			//Shape origin1 = new Box { Width = 10, Height = 20 };
			//Shape origin2 = new Circle { Radius = 30 };

			//string json1 = JsonSerializer.Serialize(origin1, options);
			//string json2 = JsonSerializer.Serialize(origin2, options);

			//Console.WriteLine(json1); // {"Tag":"Box","Width":10,"Height":20}
			//Console.WriteLine(json2); // {"Tag":"Circle","Radius":30}

			//var restored1 = JsonSerializer.Deserialize<Shape>(json1, options);
			//var restored2 = JsonSerializer.Deserialize<Shape>(json2, options);

			//Console.WriteLine(restored1); // Box: Width=10, Height=20
			//Console.WriteLine(restored2); // Circle: Radius=30	}

		}
	}
}
