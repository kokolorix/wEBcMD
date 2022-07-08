using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace wEBcMD
{
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
					return Value.GetType().FullName;
				}
			}

			public override JsonDocument ObjectValue
			{
				get
				{
					return JsonSerializer.SerializeToDocument(Value);
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
		//[JsonConverter(typeof(JsonConverterValueDTO))]
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
}
