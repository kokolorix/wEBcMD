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
		/// <summary>Indicates if this is the answer</summary>
		public virtual Boolean Response { get; set; } = false;
		/// <summary>Arguments of the command</summary>
		public virtual List<PropertyDTO> Arguments { get; set; }
	};

	/// <summary>SampleCommand</summary>
	public partial class SampleCommand : CommandWrapper
	{
		/// <summary>Constructor of SampleCommand</summary>
		public SampleCommand(CommandDTO dto):base(dto){}
		/// <summary>e3e185bd-5237-4574-977f-a040bbe12d35 is the Id of SampleCommand type.</summary>
		public static Guid TypeId { get => Guid.Parse("e3e185bd-5237-4574-977f-a040bbe12d35"); }
		/// <summary>Checks if the type of the DTO fits</summary>
		public static bool IsForMe(CommandDTO dto) => dto.Type == SampleCommand.TypeId;
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand(CommandDTO dto) => new SampleCommand(dto).ExecuteCommand();
		/// <summary>Execute the command</summary>
		public partial CommandDTO ExecuteCommand();
		/// <summary>FirstOne</summary>
		public String FirstOne {
			get => this.String["FirstOne"];
			set => this.String["FirstOne"] = value;
		}
		/// <summary>SecondOne</summary>
		public Boolean SecondOne {
			get => this.Boolean["SecondOne"];
			set => this.Boolean["SecondOne"] = value;
		}
	};

	/// <summary>SampleCommand2</summary>
	public partial class SampleCommand2 : CommandWrapper
	{
		/// <summary>Constructor of SampleCommand2</summary>
		public SampleCommand2(CommandDTO dto):base(dto){}
		/// <summary>cb0ab4ab-9aeb-4b42-ac2d-152a4555370a is the Id of SampleCommand2 type.</summary>
		public static Guid TypeId { get => Guid.Parse("cb0ab4ab-9aeb-4b42-ac2d-152a4555370a"); }
		/// <summary>Checks if the type of the DTO fits</summary>
		public static bool IsForMe(CommandDTO dto) => dto.Type == SampleCommand2.TypeId;
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand(CommandDTO dto) => new SampleCommand2(dto).ExecuteCommand();
		/// <summary>Execute the command</summary>
		public partial CommandDTO ExecuteCommand();
		/// <summary>FirstOne</summary>
		public String FirstOne {
			get => this.String["FirstOne"];
			set => this.String["FirstOne"] = value;
		}
		/// <summary>SecondOne</summary>
		public Boolean SecondOne {
			get => this.Boolean["SecondOne"];
			set => this.Boolean["SecondOne"] = value;
		}
	};

}
