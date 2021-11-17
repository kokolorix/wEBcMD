using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace wEBcMD
{
   public partial class GetObjectTypesWrapper : CommandWrapper
   {
      /// <summary>
      /// </summary>

		public partial List<ObjectTypeDTO> GetObjectTypes()
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
			ObjectTypeDTO dto = new();
			string jsonString = JsonSerializer.Serialize(dto);


			string[] objTypes = Directory.GetFiles("../Storage/ObjectTypes", "*.json");
			foreach (string type in objTypes)
			{
				var json = File.ReadAllText(type);
				var dict = JsonSerializer.Deserialize<Dictionary<string, string[]>>(json);
			}
			return default;
		}
	};

}
