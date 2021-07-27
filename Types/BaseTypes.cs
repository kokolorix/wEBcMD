using System;
using System.Collections.Generic;

namespace wEBcMD
{
	/// <summary>BaseDTO</summary>
	public partial class BaseDTO
	{
		/// <summary>Id</summary>
		public virtual  Guid Id { get; set; }
		/// <summary>Type</summary>
		public virtual  Guid Type { get; set; }
	};

	/// <summary>PropertyDTO</summary>
	public class PropertyDTO
	{
		/// <summary>Name</summary>
		public virtual String Name { get; set; }
		/// <summary>Value</summary>
		public virtual String Value { get; set; }
	};

	/// <summary>ObjectDTO</summary>
	public class ObjectDTO : BaseDTO
	{
		/// <summary>Properties</summary>
		public virtual List<PropertyDTO> Properties { get; set; }
	};

	/// <summary>TypeDTO</summary>
	public class TypeDTO : BaseDTO
	{
	};

	/// <summary>PropertyTypeDTO</summary>
	public class PropertyTypeDTO : TypeDTO
	{
		/// <summary>Name</summary>
		public virtual String Name { get; set; }
		/// <summary>DataType</summary>
		public virtual String DataType { get; set; }
		/// <summary>Default</summary>
		public virtual String Default { get; set; }
	};

	/// <summary>ObjectTypeDTO</summary>
	public class ObjectTypeDTO : TypeDTO
	{
		/// <summary>Category</summary>
		public virtual String Category { get; set; }
		/// <summary>Name</summary>
		public virtual String Name { get; set; }
		/// <summary>PropertyTypes</summary>
		public virtual List<PropertyTypeDTO> PropertyTypes { get; set; }
	};

}
