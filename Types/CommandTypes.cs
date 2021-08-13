using System;
using System.Collections.Generic;

namespace wEBcMD
{
	/// <summary>
	/// Base command object. Has all arguments in generig list, can be wrapped by concrete command wrappers
	/// </summary>
	public class CommandDTO : BaseDTO
	{
		/// <summary>c1eda1fc-cc45-4658-889f-ccd989c2848a is the Id of CommandDTO type.</summary>
		new public static Guid TypeId { get => Guid.Parse("c1eda1fc-cc45-4658-889f-ccd989c2848a"); }
		/// <summary>Indicates if this is the answer</summary>
		public virtual Boolean Response { get; set; } = false;
		/// <summary>Arguments of the command</summary>
		public virtual List<PropertyDTO> Arguments { get; set; } = new (){};
	};

	/// <summary>
	/// 
	/// This is the sample caommand. He has two Parameters
	/// and a multiline summary.
	/// ``` typescript
	/// CommandDTO cmd;
	/// if(SampleCommand.IsForMe(dto)){
	/// let sample = new SampleCommand(cmd);
	/// console.log(sample.FirstOne);
	/// }
	/// ```
	/// 
	/// </summary>
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
		/// <summary>
		/// 
		/// The FirstOne is a string parameter
		/// and has a multiline comment
		/// 
		/// </summary>
		public String FirstOne {
			get => this.String["FirstOne"];
			set => this.String["FirstOne"] = value;
		}
		/// <summary>The SecondOne is a boolean parameter</summary>
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
