using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace wEBcMD
{
   public partial class BaseDTO
   {
      /// <summary>get string from Property-List</summary>
      protected string StringFromPropertyList(List<PropertyDTO> list, string name)
      {
         return list.Find(i => i.Name == name)?.Value;
      }
      /// <summary>
      /// Set string to Property-List.
      /// If property not exists, it is newly created.
      /// Otherwise the existing value is updated.
      /// </summary>
      protected void StringToPropertyList(List<PropertyDTO> list, string name, string newValue)
      {
         var p = list.Find(i => i.Name == name);
         if (null == p)
            list.Add(new PropertyDTO() { Name = name, Value = newValue });
         else
            p.Value = newValue;
      }
      /// <summary>get Boolean from Property-List</summary>
      protected Boolean BooleanFromPropertyList(List<PropertyDTO> list, string name)
      {
         return Boolean.Parse(list.Find(i => i.Name == name)?.Value);
      }
      /// <summary>
      /// Set Boolean to Property-List.
      /// If property not exists, it is newly created.
      /// Otherwise the existing value is updated.
      /// </summary>
      protected void BooleanToPropertyList(List<PropertyDTO> list, string name, Boolean newValue)
      {
         var p = list.Find(i => i.Name == name);
         if (null == p)
            list.Add(new PropertyDTO() { Name = name, Value = newValue.ToString() });
         else
            p.Value = newValue.ToString();
      }
   }


	public class ValueDTO
	{
		public class ValueImplDTO<T> : ValueDTO
		{
			public ValueImplDTO(T v) { Value = v; }
			[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
			public T Value { get; set; }

			public override string ObjectType
			{
				get
				{
					if(Value is BaseDTO)
						return Value.GetType().Name;
					return null;
				}
			}

			public override JsonDocument ObjectValue
			{
				get
				{
					if (Value is BaseDTO)
						return JsonSerializer.SerializeToDocument(Value);
					return null;
				}
			}
		}
		public static ValueImplDTO<T> Create<T>(T v)
		{
			return new ValueDTO.ValueImplDTO<T>(v);
		}

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Int32? Int32Value
		{
			get
			{
				switch (this)
				{
					case ValueImplDTO<Int32>:
						return ((ValueImplDTO<Int32>)this).Value;
					default:
						return null;
				}
			}
		}
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Int64? Int64Value
		{
			get
			{
				switch (this)
				{
					case ValueImplDTO<Int64>:
						return ((ValueImplDTO<Int64>)this).Value;
					default:
						return null;
				}
			}
		}
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Double? DoubleValue
		{
			get
			{
				switch (this)
				{
					case ValueImplDTO<Double>:
						return ((ValueImplDTO<Double>)this).Value;
					default:
						return null;
				}
			}
		}
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Boolean? BooleanValue
		{
			get
			{
				switch (this)
				{
					case ValueImplDTO<Boolean>:
						return ((ValueImplDTO<Boolean>)this).Value;
					default:
						return null;
				}
			}
		}
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String StringValue
		{
			get
			{
				switch (this)
				{
					case ValueImplDTO<String>:
						return ((ValueImplDTO<String>)this).Value;
					default:
						return null;
				}
			}
		}

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual String ObjectType
		{
			get
			{
				switch (this)
				{
					case ValueImplDTO<BaseDTO>:
						return ((ValueImplDTO<BaseDTO>)this).Value.GetType().FullName;
					default:
						return null;
				}
			}
		}


		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public virtual JsonDocument ObjectValue
		{
			get
			{
				switch (this)
				{
					case ValueImplDTO<BaseDTO>:
						{
							BaseDTO val = ((ValueImplDTO<BaseDTO>)this).Value;
							switch (val)
							{
								case AdressDTO:
									return JsonSerializer.SerializeToDocument((AdressDTO)val);
								default:
									return JsonSerializer.SerializeToDocument(val);
							}
						}

					default:
						return null;
				}
			}
		}

		public override string ToString()
		{
			switch (this)
			{
				case ValueImplDTO<Int32>:
					return ((ValueImplDTO<Int32>)this).Value.ToString();

				case ValueImplDTO<Int64>:
					return ((ValueImplDTO<Int64>)this).Value.ToString();

				case ValueImplDTO<Double>:
					return ((ValueImplDTO<Double>)this).Value.ToString();

				case ValueImplDTO<Boolean>:
					return ((ValueImplDTO<Boolean>)this).Value.ToString();

				case ValueImplDTO<String>:
					return ((ValueImplDTO<String>)this).Value.ToString();

				case ValueImplDTO<BaseDTO>:
					return ((ValueImplDTO<BaseDTO>)this).Value.ToString();

				default:
					return String.Empty;
			}
		}
	}

	public class ArgDTO
	{
		public String Name { get; set; }

		public ValueDTO Value { get; set; }

	}

}
