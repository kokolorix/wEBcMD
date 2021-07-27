using System;
using System.Collections.Generic;

namespace wEBcMD
{
	/// <summary>
	/// Base command object. Has all arguments in generig list, can be derived by concrete command types
	/// </summary>
	public class CommandDTO : BaseDTO
	{
		/// <summary>Indicates if this is the answer</summary>
		public virtual Boolean Response { get; set; } = false;
		/// <summary>Arguments of the command</summary>
		public virtual List<PropertyDTO> Arguments { get; set; }
	};

	/// <summary>SampleCommandDTO</summary>
	public class SampleCommandDTO : CommandDTO
	{
		/// <summary>FirstOne</summary>
		public virtual String FirstOne {
			get { return StringFromPropertyList( this.Arguments, "FirstOne" ); }
			set { StringToPropertyList( this.Arguments, "FirstOne", value ); }
		}
		/// <summary>SecondOne</summary>
		public virtual Boolean SecondOne { get; set; }
	};

}
