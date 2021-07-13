using System;
using System.Collections.Generic;

namespace wEBcMD
{
	public class PropertyDTO
	{
		public String Name { get; set; }
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

	public class PropertyTypeDTO : TypeDTO
	{
		public String Name { get; set; }
		public String DataType { get; set; }
		public String Default { get; set; }
	};

	public class ObjectTypeDTO : TypeDTO
	{
		public String Category { get; set; }
		public String Name { get; set; }
		public List<PropertyTypeDTO> PropertyTypes { get; set; }
	};

}
