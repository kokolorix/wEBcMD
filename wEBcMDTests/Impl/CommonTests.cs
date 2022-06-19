using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace wEBcMD.Tests
{
	[TestClass]
	public class CommonTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			JsonSerializerOptions options = new JsonSerializerOptions();

			ValueDTO val1 = ValueDTO.Create(42);

			String json = JsonSerializer.Serialize(val1);

			ValueDTO val2 = JsonSerializer.Deserialize<ValueDTO>(json);	
			Console.WriteLine(JsonSerializer.Serialize(val2));



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
