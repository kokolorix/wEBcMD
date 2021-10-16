using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace wEBcMD
{

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
   }
}
