using System;
using System.Collections.Generic;
#pragma warning disable 1591

namespace wEBcMD
{
   /// <summary>
   /// Base for all wrappers
   /// </summary>
   /// <example>
   ///
   /// </example>
   public class ObjectWrapper
   {
      protected ObjectDTO _dto;

      public ObjectWrapper(ObjectDTO dto) => _dto = dto;
      public struct StringValues
      {
         private readonly List<PropertyDTO> _list;
         public StringValues(List<PropertyDTO> list) => _list = list;
         public string this[string propertyName]
         {
            get => _list.Find(p => p.Name == propertyName)?.Value;
            set
            {
               var p = _list.Find(i => i.Name == propertyName);
               if (null == p)
                  _list.Add(new PropertyDTO() { Name = propertyName, Value = value });
               else
                  p.Value = value;
            }
         }
      }
      public virtual StringValues String{
         get => new(_dto.Properties);
      }
   }

   public class CommandWrapper
   {
      public CommandDTO Cmd {get; init; }

      public CommandWrapper(CommandDTO dto) => Cmd = dto;
      public class StringValues
      {
         private readonly List<PropertyDTO> _list;
         public StringValues(List<PropertyDTO> list) => _list = list;
         public string this[string name]
         {
            get => _list.Find(p => p.Name == name)?.Value;
            set
            {
               var p = _list.Find(i => i.Name == name);
               if (null == p)
                  _list.Add(new PropertyDTO() { Name = name, Value = value });
               else
                  p.Value = value;
            }
         }
      }
      public StringValues String{
         get => new(Cmd.Arguments);
      }

      public class BooleanValues
      {
         private readonly List<PropertyDTO> _list;
         public BooleanValues(List<PropertyDTO> list) => _list = list;
         public System.Boolean this[string name]
         {
            get => System.Boolean.Parse(_list.Find(p => p.Name == name)?.Value);
            set
            {
               var p = _list.Find(i => i.Name == name);
               if (null == p)
                  _list.Add(new PropertyDTO() { Name = name, Value = value.ToString() });
               else
                  p.Value = value.ToString();
            }
         }
      }
      public BooleanValues Boolean{
         get => new(Cmd.Arguments);
      }
   }
}
