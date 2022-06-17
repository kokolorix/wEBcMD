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


	public record ValueDTO
	{
		public static ValueDTO Create(Int32 v) => new ValueDTO.NumberValueDTO.Int32ValueDTO(v);
		public static ValueDTO Create(Int64 v) => new ValueDTO.NumberValueDTO.Int64ValueDTO(v);
		public static ValueDTO Create(Double v) => new ValueDTO.NumberValueDTO.FloatValueDTO(v);
		public static ValueDTO Create(String v) => new ValueDTO.NumberValueDTO.StringValueDTO(v);
		public static ValueDTO Create(Boolean v) => new ValueDTO.NumberValueDTO.BooleanValueDTO(v);
		public static ValueDTO Create(BaseDTO v) => new ValueDTO.NumberValueDTO.ObjectValueDTO(v);

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Int32? Int32Value
		{
			get
			{
				switch (this)
				{
					case NumberValueDTO.Int32ValueDTO:
						return ((NumberValueDTO.Int32ValueDTO)this).Value;
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
					case NumberValueDTO.Int64ValueDTO:
						return ((NumberValueDTO.Int64ValueDTO)this).Value;
					default:
						return null;
				}
			}
		}
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Double? FloatValue
		{
			get
			{
				switch (this)
				{
					case NumberValueDTO.FloatValueDTO:
						return ((NumberValueDTO.FloatValueDTO)this).Value;
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
					case NumberValueDTO.BooleanValueDTO:
						return ((NumberValueDTO.BooleanValueDTO)this).Value;
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
					case NumberValueDTO.StringValueDTO:
						return ((NumberValueDTO.StringValueDTO)this).Value;
					default:
						return null;
				}
			}
		}

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public String ObjectType
		{
			get
			{
				switch (this)
				{
					case NumberValueDTO.ObjectValueDTO:
						return ((NumberValueDTO.ObjectValueDTO)this).Value.GetType().Name;
					default:
						return null;
				}
			}
		}


		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public JsonDocument ObjectValue
		{
			get
			{
				switch (this)
				{
					case NumberValueDTO.ObjectValueDTO:
						{
							BaseDTO val = ((NumberValueDTO.ObjectValueDTO)this).Value;
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
				case NumberValueDTO.Int32ValueDTO:
					return ((NumberValueDTO.Int32ValueDTO)this).Value.ToString();

				case NumberValueDTO.Int64ValueDTO:
					return ((NumberValueDTO.Int64ValueDTO)this).Value.ToString();

				case NumberValueDTO.FloatValueDTO:
					return ((NumberValueDTO.FloatValueDTO)this).Value.ToString();

				case NumberValueDTO.BooleanValueDTO:
					return ((NumberValueDTO.BooleanValueDTO)this).Value.ToString();

				case NumberValueDTO.StringValueDTO:
					return ((NumberValueDTO.StringValueDTO)this).Value.ToString();

				case NumberValueDTO.ObjectValueDTO:
					return ((NumberValueDTO.ObjectValueDTO)this).Value.ToString();

				default:
					return String.Empty;
			}
		}

		public record NumberValueDTO : ValueDTO
		{
			public NumberValueDTO() { }
			public NumberValueDTO(Int64 i) : this() { }

			public record Int32ValueDTO(Int32 Value) : NumberValueDTO;
			public record Int64ValueDTO(Int64 Value) : NumberValueDTO;
			public record FloatValueDTO(Double Value) : NumberValueDTO;
		};
		public record BooleanValueDTO(Boolean Value) : ValueDTO();
		public record StringValueDTO(String Value) : ValueDTO();
		public record ObjectValueDTO(BaseDTO Value) : ValueDTO();
	}

	public class ArgDTO
	{
		public String Name { get; set; }

		public ValueDTO Value { get; set; }

	}

}
