using System;
using System.Collections.Generic;

namespace wEBcMD
{
	/// <summary>BaseDTO</summary>
	public partial class BaseDTO
	{
		/// <summary>1a81bc99-28c2-4c03-ac5c-a1de4967cc36 is the Id of BaseDTO type.</summary>
		public static Guid TypeId { get => Guid.Parse("1a81bc99-28c2-4c03-ac5c-a1de4967cc36"); }
		/// <summary>Id</summary>
		public virtual  Guid Id { get; set; }
		/// <summary>Type</summary>
		public virtual  Guid Type { get; set; }
	};

	/// <summary>PropertyDTO</summary>
	public class PropertyDTO
	{
		/// <summary>24ef4fd2-e337-4baa-89dd-404a72200871 is the Id of PropertyDTO type.</summary>
		public static Guid TypeId { get => Guid.Parse("24ef4fd2-e337-4baa-89dd-404a72200871"); }
		/// <summary>Name</summary>
		public virtual String Name { get; set; }
		/// <summary>Value</summary>
		public virtual String Value { get; set; }
	};

	/// <summary>ObjectDTO</summary>
	public class ObjectDTO : BaseDTO
	{
		/// <summary>1a81bc99-28c2-4c03-ac5c-a1de4967cc36 is the Id of ObjectDTO type.</summary>
		new public static Guid TypeId { get => Guid.Parse("1a81bc99-28c2-4c03-ac5c-a1de4967cc36"); }
		/// <summary>Properties</summary>
		public virtual List<PropertyDTO> Properties { get; set; }
	};

	/// <summary>TypeDTO</summary>
	public class TypeDTO : BaseDTO
	{
		/// <summary>2c8c1feb-0d04-45d2-bbe7-fe137450412e is the Id of TypeDTO type.</summary>
		new public static Guid TypeId { get => Guid.Parse("2c8c1feb-0d04-45d2-bbe7-fe137450412e"); }
	};

	/// <summary>PropertyTypeDTO</summary>
	public class PropertyTypeDTO : TypeDTO
	{
		/// <summary>b070edd3-f270-4093-a500-94047d18c7f9 is the Id of PropertyTypeDTO type.</summary>
		new public static Guid TypeId { get => Guid.Parse("b070edd3-f270-4093-a500-94047d18c7f9"); }
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
		/// <summary>38b38794-0a2e-466c-b198-c831708298f6 is the Id of ObjectTypeDTO type.</summary>
		new public static Guid TypeId { get => Guid.Parse("38b38794-0a2e-466c-b198-c831708298f6"); }
		/// <summary>Category</summary>
		public virtual String Category { get; set; }
		/// <summary>Name</summary>
		public virtual String Name { get; set; }
		/// <summary>PropertyTypes</summary>
		public virtual List<PropertyTypeDTO> PropertyTypes { get; set; }
	};

}
