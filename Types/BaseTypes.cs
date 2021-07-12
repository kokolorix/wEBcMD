using System;
using System.Collections.Generic;

namespace wEBcMD
{
	public class PropertyDTO
	{
		public String Name { get; set; }
		public String Type { get; set; }
		public String Value { get; set; }
	};

	public class BaseDTO
	{
		public  Guid Id { get; set; }
		public  Guid Type { get; set; }
		public List<PropertyDTO> Properties { get; set; }
	};

	public class TypeDTO : BaseDTO
	{
	};

	public class TextDTO : TypeDTO
	{
		public String Encoding { get; set; }
		public UInt64 MaxLength { get; set; }
	};

}
