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
      public new static Guid TypeId { get => System.Guid.Parse("c1eda1fc-cc45-4658-889f-ccd989c2848a"); }
      /// <summary>Indicates if this is the answer</summary>
      public virtual Boolean Response { get; set; } = false;
      /// <summary>Arguments of the command</summary>
      public virtual List<PropertyDTO> Arguments { get; set; } = new (){};
   };

   /// <summary>ParamDTO</summary>
   public class ParamDTO
   {
      /// <summary>95f50f9b-9e6c-4f72-8bfe-486adffda5a9 is the Id of ParamDTO type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("95f50f9b-9e6c-4f72-8bfe-486adffda5a9"); }
      /// <summary>Name</summary>
      public virtual String Name { get; set; }
      /// <summary>Type</summary>
      public virtual String Type { get; set; }
   };

   /// <summary>CommandTypeDTO</summary>
   public class CommandTypeDTO : TypeDTO
   {
      /// <summary>7e4e81c9-9170-4f9e-bfe0-b9acd359958b is the Id of CommandTypeDTO type.</summary>
      public new static Guid TypeId { get => System.Guid.Parse("7e4e81c9-9170-4f9e-bfe0-b9acd359958b"); }
      /// <summary>Name</summary>
      public virtual String Name { get; set; }
      /// <summary>Result</summary>
      public virtual String Result { get; set; }
      /// <summary>Parameters</summary>
      public virtual List<ParamDTO> Parameters { get; set; }
   };

   /// <summary>
   /// Get a list of all Command-Types
   /// </summary>
   public partial class GetCommandTypesWrapper : CommandWrapper
   {
      /// <summary>Constructor of GetCommandTypesWrapper</summary>
      public GetCommandTypesWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>6dab2a85-0256-421c-8a7a-2337453a3e48 is the Id of GetCommandTypesWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("6dab2a85-0256-421c-8a7a-2337453a3e48"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == GetCommandTypesWrapper.TypeId;

		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			GetCommandTypesWrapper wrapper = new(dto);
			
			wrapper.Result = 
			wrapper.GetCommandTypes(
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// Get a list of all Command-Types
      /// </summary>
		public partial List<CommandTypeDTO> GetCommandTypes();

		/// <summary>Serialize / Deserialize concrete GetCommandTypes to generic CommandDTO </summary>
		public override CommandDTO Cmd
		{
			get
			{
				CommandDTO cmd = base.Cmd;
				
				this.Set(cmd, "Result", Result);
				
				cmd.Response = true;
				return cmd;
			}
			set
			{
				CommandDTO cmd = value;
				
				
				base.Cmd = cmd;
				cmd.Response = false;
			}
		}
      /// <summary>The command type object</summary>

		public List<CommandTypeDTO>
		Result { get; set; }
   };

   /// <summary>
   /// 
   /// This is the sample command. He has two Parameters
   /// and a multiline summary.
   /// ``` typescript
   /// CommandDTO cmd;
   /// if(SampleCommand.IsForMe(dto)){ let sample = new SampleCommand(cmd); console.log(sample.FirstOne);
   /// }
   /// ```
   /// 
   /// </summary>
   public partial class SampleCommandWrapper : CommandWrapper
   {
      /// <summary>Constructor of SampleCommandWrapper</summary>
      public SampleCommandWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>e3e185bd-5237-4574-977f-a040bbe12d35 is the Id of SampleCommandWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("e3e185bd-5237-4574-977f-a040bbe12d35"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == SampleCommandWrapper.TypeId;

		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			SampleCommandWrapper wrapper = new(dto);
			
			wrapper.SampleCommand(
					wrapper.FirstOne, 
					wrapper.SecondOne
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// 
      /// This is the sample command. He has two Parameters
      /// and a multiline summary.
      /// ``` typescript
      /// CommandDTO cmd;
      /// if(SampleCommand.IsForMe(dto)){ let sample = new SampleCommand(cmd); console.log(sample.FirstOne);
      /// }
      /// ```
      /// 
      /// </summary>
		public partial void SampleCommand(String firstOne, Boolean secondOne);

		/// <summary>Serialize / Deserialize concrete SampleCommand to generic CommandDTO </summary>
		public override CommandDTO Cmd
		{
			get
			{
				CommandDTO cmd = base.Cmd;
				
				this.Set(cmd, "FirstOne", FirstOne);
				this.Set(cmd, "SecondOne", SecondOne);
				cmd.Response = true;
				return cmd;
			}
			set
			{
				CommandDTO cmd = value;
				
				this.Get(cmd, "FirstOne",  (()=>this.FirstOne, x => this.FirstOne	= x));
				this.Get(cmd, "SecondOne",  (()=>this.SecondOne, x => this.SecondOne	= x));
				
				base.Cmd = cmd;
				cmd.Response = false;
			}
		}
      /// <summary>
      /// 
      /// The FirstOne is a string parameter
      /// and has a multiline comment
      /// </summary>
      public String FirstOne { get; set; }
      /// <summary>The SecondOne is a boolean parameter</summary>
      public Boolean SecondOne { get; set; }

   };


   static partial class CommandTypes
   {
      public static CommandDTO Dispatch(CommandDTO dto)
      {
         if (null == dto)
            return dto;
         
         else if(GetCommandTypesWrapper.IsForMe(dto))
            return GetCommandTypesWrapper.ExecuteCommand(dto);
         
         else if(SampleCommandWrapper.IsForMe(dto))
            return SampleCommandWrapper.ExecuteCommand(dto);
         
         return null;
      }
   }

	///<summary>Types from this module</summary>
   static partial class CommandTypes
   {
		///<summary>List of cref="CommandTypeDTO" from this module</summary>
      public static void GetTypes(ref List<CommandTypeDTO> commandTypes)
      {
			commandTypes.AddRange(new CommandTypeDTO[] {
				new() {
					Name = "GetCommandTypes",
					Result = "CommandTypeDTO[]",
					Parameters = new List<ParamDTO>()
					{
						
					},
					Id = Guid.Parse("6dab2a85-0256-421c-8a7a-2337453a3e48"),
					Type = CommandTypeDTO.TypeId,
				},
				new() {
					Name = "SampleCommand",
					Result = "Void",
					Parameters = new List<ParamDTO>()
					{
						new() {
							Name="FirstOne",
							Type="String",
						},new() {
							Name="SecondOne",
							Type="Boolean",
						},
					},
					Id = Guid.Parse("e3e185bd-5237-4574-977f-a040bbe12d35"),
					Type = CommandTypeDTO.TypeId,
				},
			});
      }
   }
}
