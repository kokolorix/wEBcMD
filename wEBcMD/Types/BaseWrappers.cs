using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
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
   }

   public class CommandWrapper
   {
      public virtual CommandDTO Cmd {get; set; }

      public CommandWrapper(CommandDTO dto) {
         Cmd = dto ?? new();
      }

      protected void Get<T>(CommandDTO cmd, string name, (Func<T> get, Action<T> set) target)
      {
         string jsonString = cmd.Arguments.Find(p => p.Name == name)?.Value.StringValue;
         if (!string.IsNullOrEmpty(jsonString))
            target.set(JsonSerializer.Deserialize<T>(jsonString));
         else
            target.set(default);
      }
      protected void Get(CommandDTO cmd, string name, (Func<Guid> get, Action<Guid> set) target)
      {
         String jsonString = cmd.Arguments.Find(p => p.Name == name)?.Value.StringValue;
         if (!String.IsNullOrEmpty(jsonString))
            target.set(Guid.Parse(jsonString.Replace("\"", "")));
         else
            target.set(default);
      }

      protected void Set<T>(CommandDTO cmd, string name, T value)
      {
         string jsonString = JsonSerializer.Serialize(value);
         var p = cmd.Arguments.Find(i => i.Name == name);
         if (null == p)
            cmd.Arguments.Add(new PropertyDTO() { Name = name, Value = ValueDTO.Create(jsonString) });
         else
            p.Value = ValueDTO.Create(jsonString);
      }
   }
}
