using System.Collections.Generic;

namespace wEBcMD
{
   public partial class BaseDTO
   {
      /// <summary>get string from Property-List</summary>
      protected string StringFromPropertyList(List<PropertyDTO> list, string name){
         return list.Find( i => i.Name==name)?.Value;
      }
      /// <summary>
      /// Set string to Property-List.
      /// If property not exists, it is newly created.
      /// Otherwise the existing value is updated.
      /// </summary>
      protected void StringToPropertyList(List<PropertyDTO> list, string name, string newValue){
         var p = list.Find( i => i.Name==name);
         if(null == p)
            list.Add(new PropertyDTO() { Name = name, Value = newValue });
         else
            p.Value = newValue;
      }
   }
}
