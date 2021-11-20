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
		 ObjectTypeDTO dto = new(){
			 Name="ObyTypeA",
			PropertyTypes = new()
			{
			   new() { Name="PropertyA", Type="String"}
			}
		 };
		 string jsonString = JsonSerializer.Serialize(dto);

		 List<ObjectTypeDTO> res = new();

			string[] objTypeFiles = Directory.GetFiles("../Storage/ObjectTypes", "*.json");
			foreach (string filePath in objTypeFiles)
			{
				var jsonStr = File.ReadAllText(filePath);

				var jdoc = JsonDocument.Parse(jsonStr);
				if (jdoc.RootElement.ValueKind == JsonValueKind.Object)
				{
					var objType = JsonSerializer.Deserialize<ObjectTypeDTO>(jsonStr);
					res.Add(objType);
					//var obj = (ObjectTypeDTO)jdoc.RootElement;
				}
				else if (jdoc.RootElement.ValueKind == JsonValueKind.Array)
				{
					var objTypes = JsonSerializer.Deserialize<List<ObjectTypeDTO>>(jsonStr);
					res.AddRange(objTypes);
					//var obj = (ObjectTypeDTO)jdoc.RootElement;
				}
				//	res.AddRange(jdoc.RootElement.)
				//using (var json = File.OpenText(filePath))
				//using (var reader = new JsonReader(json))
				//{
				//	var phones = DeserializeSingleOrList<Phone>(reader);

					//	return Request.CreateResponse<List<Phone>>(HttpStatusCode.OK, phones);
					//}

			}
			return res;
		}
	};

}
