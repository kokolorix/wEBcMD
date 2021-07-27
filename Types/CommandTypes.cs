using System;
using System.Collections.Generic;

namespace wEBcMD
{
	/// <summary>
	/// Base command object. Has all arguments in generig list, can be derived by concrete command types
	/// </summary>
	public class CommandDTO : BaseDTO
	{
		/// <summary>c1eda1fc-cc45-4658-889f-ccd989c2848a is the Id of CommandDTO type.</summary>
		new public static Guid TypeId { get => Guid.Parse("c1eda1fc-cc45-4658-889f-ccd989c2848a"); }
		/// <summary>Id of CommandDTO type.</summary>
		public override Guid Type { get => CommandDTO.TypeId; }
		/// <summary>Indicates if this is the answer</summary>
		public virtual Boolean Response { get; set; } = false;
		/// <summary>Arguments of the command</summary>
		public virtual List<PropertyDTO> Arguments { get; set; }
	};

	/// <summary>SampleCommandDTO</summary>
	public class SampleCommandDTO : CommandDTO
	{
		/// <summary>e3e185bd-5237-4574-977f-a040bbe12d35 is the Id of SampleCommandDTO type.</summary>
		new public static Guid TypeId { get => Guid.Parse("e3e185bd-5237-4574-977f-a040bbe12d35"); }
		/// <summary>Id of SampleCommandDTO type.</summary>
		public override Guid Type { get => SampleCommandDTO.TypeId; }
		/// <summary>FirstOne</summary>
		public virtual String FirstOne {
			get { return StringFromPropertyList( this.Arguments, "FirstOne" ); }
			set { StringToPropertyList( this.Arguments, "FirstOne", value ); }
		}
		/// <summary>SecondOne</summary>
		public virtual Boolean SecondOne {
			get { return BooleanFromPropertyList( this.Arguments, "SecondOne" ); }
			set { BooleanToPropertyList( this.Arguments, "SecondOne", value ); }
		}
	};

}
