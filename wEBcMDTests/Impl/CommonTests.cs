using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static wEBcMD.ValueDTO;

namespace wEBcMD.Tests
{
	[TestClass]
	public class CommonTests
	{
		void Test<T>(T v, JsonSerializerOptions options)
		{
			ValueDTO val1 = ValueDTO.Create(v);
			String json1 = JsonSerializer.Serialize(val1, options);
			Console.WriteLine("Serialize: {0}", json1);

			ValueDTO val2 = JsonSerializer.Deserialize<ValueDTO>(json1, options);	
			String json2 = JsonSerializer.Serialize(val2, options);
			Console.WriteLine("Deserialized: {0}", json2);

			var v1 = ((ValueImplDTO<T>)val1).Value;
			var v2 = ((ValueImplDTO<T>)val2).Value;

			//var cmp = new System.Collections.Comparer();
			Assert.AreEqual(json1, json2);
			//Assert.AreEqual(((ValueImplDTO<T>)val1).Value, ((ValueImplDTO<T>)val2).Value);
		}

		[TestMethod]
		public void TestMethod1()
		{
			JsonSerializerOptions options = new JsonSerializerOptions();
			options.AllowTrailingCommas = true;
			options.Converters.Add(new JsonConverterValueDTO());
			 
			Test<Int32>(42, options);
			Test<Int64>(Int64.MaxValue, options);
			Test<Double>(3.14, options);
			Test<Boolean>(true, options);
			Test<String>("Hallihallo", options);
			Test<AdressDTO>(new()
			{
				Id = Guid.NewGuid(),
				Name1 = "Name1", Name2= "Name2",
				Adress1="Adress1",
				City="City", Postcode="0000",
				Housenumber="1a"
			}, options);


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
