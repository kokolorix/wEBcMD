using System;
using System.Collections.Generic;
using System.Text.Json;
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
         this._string = new(Cmd.Arguments);
         this._boolean = new(Cmd.Arguments);
         this._guid = new(Cmd.Arguments);
         this._baseDTO = new(Cmd.Arguments);
      }

      protected void Get<T>(CommandDTO cmd, string name, (Func<T> get, Action<T> set) target)
      {
         string jsonString = cmd.Arguments.Find(p => p.Name == name)?.Value;
         if (!string.IsNullOrEmpty(jsonString))
            target.set(JsonSerializer.Deserialize<T>(jsonString));
         else
            target.set(default(T));
      }
      protected void Get(CommandDTO cmd, string name, (Func<Guid> get, Action<Guid> set) target)
      {
         string jsonString = cmd.Arguments.Find(p => p.Name == name)?.Value;
         if (!string.IsNullOrEmpty(jsonString))
            target.set(Guid.Parse(jsonString));
         else
            target.set(default(Guid));
      }

      protected void Set<T>(CommandDTO cmd, string name, T value)
      {
         string jsonString = JsonSerializer.Serialize(value);
         var p = cmd.Arguments.Find(i => i.Name == name);
         if (null == p)
            cmd.Arguments.Add(new PropertyDTO() { Name = name, Value = jsonString });
         else
            p.Value = jsonString;
      }
   }
}
