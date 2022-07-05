using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using static wEBcMD.ValueDTO;

namespace wEBcMD
{
	public class JsonConverterValueDTO : JsonConverter<ValueDTO>
	{
		private readonly static JsonConverterValueDTO _converter = new();
		public static JsonConverterValueDTO Instance => _converter;
		/// <summary>Write the appropriate structure for ValueDTO</summary>
		public override void Write(Utf8JsonWriter writer, ValueDTO value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			switch (value)
			{
				case ValueImplDTO<Int32>:
					{
						writer.WritePropertyName("Int32Value");
						writer.WriteNumberValue((Int32)value.Int32Value);
						break;
					}
				case ValueImplDTO<Int64>:
					{
						writer.WritePropertyName("Int64Value");
						writer.WriteNumberValue((Int64)value.Int64Value);
						break;
					}
				case ValueImplDTO<Double>:
					{
						writer.WritePropertyName(nameof(value.DoubleValue));
						writer.WriteNumberValue((Double)value.DoubleValue);
						break;
					}
				case ValueImplDTO<Boolean>:
					{
						writer.WritePropertyName(nameof(value.BooleanValue));
						writer.WriteBooleanValue((Boolean)value.BooleanValue);
						break;
					}
				case ValueImplDTO<String>:
					{
						writer.WritePropertyName(nameof(value.StringValue));
						writer.WriteStringValue((String)value.StringValue);
						break;
					}
				default:
					{
						if(value.ObjectType is not null)
						{
							writer.WritePropertyName(nameof(value.ObjectType));
							writer.WriteStringValue((String)value.ObjectType);
						}
						if(value.ObjectValue is not null)
						{
							writer.WritePropertyName(nameof(value.ObjectValue));
							value.ObjectValue.WriteTo(writer);
						}
						break;
					}
			}

			writer.WriteEndObject();
		}
		/// <summary>Read the appropriate structure for cref="ValueDTO"</summary>
		public override ValueDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ValueDTO value = null;

			reader.Read(); // read over start of object
			string propertyName = reader.GetString();
			reader.Read(); // read over name of property

			switch (propertyName)
			{
				case nameof(value.Int32Value):
					{
						value = ValueDTO.Create(reader.GetInt32());
						break;
					}
				case nameof(value.Int64Value):
					{
						value = ValueDTO.Create(reader.GetInt64());
						break;
					}
				case nameof(value.DoubleValue):
					{
						value = ValueDTO.Create(reader.GetDouble());
						break;
					}
				case nameof(value.BooleanValue):
					{
						value = ValueDTO.Create(reader.GetBoolean());
						break;
					}
				case nameof(value.StringValue):
					{
						value = ValueDTO.Create(reader.GetString());
						break;
					}

				case nameof(value.ObjectType):
					{
						String typeName = reader.GetString();
						reader.Read();
						Type type = Type.GetType(typeName);
						if(type is null)
							type = GetType(typeName);

						dynamic objValue = Convert.ChangeType(JsonSerializer.Deserialize(ref reader, type), type);
						value = ValueDTO.Create(objValue);
						break;
					}

				default:
					{
						throw new NotImplementedException();
					}
			}

			//reader.Read(); // read over property's value

			reader.Read(); // read over end of object
			return value;
		}
		static Type GetType(string className)
		{
			//return (Type)(new CSharpCodeProvider().CompileAssemblyFromSource(new CompilerParameters(AppDomain.CurrentDomain.GetAssemblies().SelectMany<Assembly, string>(a => a.GetModules().Select<Module, string>(m => m.FullyQualifiedName)).ToArray(), null, false) { GenerateExecutable = false, GenerateInMemory = true, TreatWarningsAsErrors = false, CompilerOptions = "/optimize" }, "public static class C{public static System.Type M(){return typeof(" + friendlyName + ");}}").CompiledAssembly.GetExportedTypes()[0].GetMethod("M").Invoke(null, System.Reflection.BindingFlags.Static, null, null, null));
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Reverse())
			{
				var t = assembly.GetType(className);
				if(t is not null)
					return t;
				if(assembly.IsDynamic)
					continue;

				foreach (var tt in assembly.GetExportedTypes())
				{
					if (tt is not null && tt.Name == className)
						return tt;
				}
			}
			return null;
		}
	}

	public class JsonConverterValueImplDTO<T> : JsonConverter<ValueDTO.ValueImplDTO<T>>
	{
		public override ValueImplDTO<T> Read(ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options)
		{
			return (ValueImplDTO<T>)JsonConverterValueDTO.Instance.Read(
				ref reader, typeToConvert, options);
		}

		public override void Write(Utf8JsonWriter writer,
			ValueDTO.ValueImplDTO<T> value,
			JsonSerializerOptions options)
		{
			JsonConverterValueDTO.Instance.Write(writer, value, options);
		}
	}
};
