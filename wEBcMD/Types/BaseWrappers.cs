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
      public virtual CommandDTO Cmd {get; set; }

      public CommandWrapper(CommandDTO dto) {
         Cmd = dto ?? new();
         this._string = new(Cmd.Arguments);
         this._boolean = new(Cmd.Arguments);
         this._guid = new(Cmd.Arguments);
         this._baseDTO = new(Cmd.Arguments);
      }
      //string GetString(string name)
      //{
      //   //return this.Cmd.Arguments.Find(p => p.Name == name)?.Value;
      //   string jsonString = this.Cmd.Arguments.Find(p => p.Name == name)?.Value;
      //   if (!string.IsNullOrEmpty(jsonString))
      //      return JsonSerializer.Deserialize<string>(jsonString);
      //   return default(string);
      //}
      //Guid GetGuid(string name)      {
      //   string jsonString = this.Cmd.Arguments.Find(p => p.Name == name)?.Value;
      //   if (!string.IsNullOrEmpty(jsonString))
      //      return JsonSerializer.Deserialize<Guid>(jsonString);
      //   return default(Guid);
      //}
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

      //protected T Get<T>(CommandDTO cmd, string name)
      //{
      //   string jsonString = cmd.Arguments.Find(p => p.Name == name)?.Value;
      //   if (!string.IsNullOrEmpty(jsonString))
      //      return JsonSerializer.Deserialize<T>(jsonString);
      //   return default(T);
      //}
      protected void Set<T>(CommandDTO cmd, string name, T value)
      {
         string jsonString = JsonSerializer.Serialize(value);
         var p = cmd.Arguments.Find(i => i.Name == name);
         if (null == p)
            cmd.Arguments.Add(new PropertyDTO() { Name = name, Value = jsonString });
         else
            p.Value = jsonString;
      }

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
      protected StringValues _string;

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
      protected BooleanValues _boolean;
      public class GuidValues
      {
         private readonly List<PropertyDTO> _list;
         public GuidValues(List<PropertyDTO> list) => _list = list;
         public System.Guid this[string name]
         {
            get => System.Guid.Parse(_list.Find(p => p.Name == name)?.Value);
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
      protected GuidValues _guid;
      protected class DTOValues<T>
      {
         private readonly List<PropertyDTO> _list;
         private readonly Dictionary<string,T> _map;
         public DTOValues(List<PropertyDTO> list) {
            _list = list;
            _map = new();
         }
         ~DTOValues()
         {
            foreach(var e in _map)
            {
               string jsonString = JsonSerializer.Serialize(e.Value);
               var p = _list.Find(i => i.Name == e.Key);
               if (null == p)
                  _list.Add(new PropertyDTO() { Name = e.Key, Value = jsonString });
               else
                  p.Value = jsonString;
            }
         }
         public T this[string name]
         {
            get
            {
               if (_map.TryGetValue(name, out T dto))
                  return dto;

               string jsonString = _list.Find(p => p.Name == name)?.Value;
               if (!string.IsNullOrEmpty(jsonString))
                  dto = JsonSerializer.Deserialize<T>(jsonString);
               _map[name] = dto;
               return dto;
            }
            set
            {
               _map[name] = value;
            }
         }
      }
      protected DTOValues<BaseDTO> _baseDTO;
   }
}
