using System;
using System.Collections.Generic;

namespace wEBcMD
{
   /// <summary>
   /// Description of the Data-Strucure.
   /// </summary>
   public class ExampleDTO : BaseDTO
   {
      /// <summary>5c7fc88a-b15a-4a4b-b687-e320c44743de is the Id of ExampleDTO type.</summary>
      public new static Guid TypeId { get => System.Guid.Parse("5c7fc88a-b15a-4a4b-b687-e320c44743de"); }
      /// <summary>First property, a string</summary>
      public virtual String One { get; set; }
      /// <summary>Second one, a boolean</summary>
      public virtual Boolean Two { get; set; }
   };

   /// <summary>
   /// The very first command we designed
   /// </summary>
   public partial class ExampleWrapper : CommandWrapper
   {
      /// <summary>Constructor of ExampleWrapper</summary>
      public ExampleWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>a84ca129-1d08-4864-a97d-50639b8055d5 is the Id of ExampleWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("a84ca129-1d08-4864-a97d-50639b8055d5"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == ExampleWrapper.TypeId;

		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			ExampleWrapper wrapper = new(dto);
			
			wrapper.Result = 
			wrapper.Example(
					wrapper.Id
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// The very first command we designed
      /// </summary>
		public partial ExampleDTO Example(Guid id);

		/// <summary>Serialize / Deserialize concrete Example to generic CommandDTO </summary>
		public override CommandDTO Cmd
		{
			get
			{
				CommandDTO cmd = base.Cmd;
				
				this.Set(cmd, "Id", Id);
				this.Set(cmd, "Result", Result);
				
				cmd.Response = true;
				return cmd;
			}
			set
			{
				CommandDTO cmd = value;
				
				this.Get(cmd, "Id",  (()=>this.Id, x => this.Id	= x));
				
				base.Cmd = cmd;
				cmd.Response = false;
			}
		}
      /// <summary>The Id of the Data-Object</summary>
      public Guid Id { get; set; }
      /// <summary>The Data-Object</summary>

		public ExampleDTO
		Result { get; set; }
   };

   /// <summary>
   /// A command, that returns the given value
   /// </summary>
   public partial class EchoValueWrapper : CommandWrapper
   {
      /// <summary>Constructor of EchoValueWrapper</summary>
      public EchoValueWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>69c8864e-dc43-48ac-9865-99cac19aa5c0 is the Id of EchoValueWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("69c8864e-dc43-48ac-9865-99cac19aa5c0"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == EchoValueWrapper.TypeId;

		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			EchoValueWrapper wrapper = new(dto);
			
			wrapper.Result = 
			wrapper.EchoValue(
					wrapper.value
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// A command, that returns the given value
      /// </summary>
		public partial ValueDTO EchoValue(ValueDTO value);

		/// <summary>Serialize / Deserialize concrete EchoValue to generic CommandDTO </summary>
		public override CommandDTO Cmd
		{
			get
			{
				CommandDTO cmd = base.Cmd;
				
				this.Set(cmd, "value", value);
				this.Set(cmd, "Result", Result);
				
				cmd.Response = true;
				return cmd;
			}
			set
			{
				CommandDTO cmd = value;
				
				this.Get(cmd, "value",  (()=>this.value, x => this.value	= x));
				
				base.Cmd = cmd;
				cmd.Response = false;
			}
		}
      /// <summary>The value to deserialize</summary>
      public ValueDTO value { get; set; }
      /// <summary>The serialized value</summary>

		public ValueDTO
		Result { get; set; }
   };

   /// <summary>
   /// A command that returns the given arguments
   /// </summary>
   public partial class EchoArgumentsWrapper : CommandWrapper
   {
      /// <summary>Constructor of EchoArgumentsWrapper</summary>
      public EchoArgumentsWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>1de8430f-bd11-4747-8455-698952b8ce49 is the Id of EchoArgumentsWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("1de8430f-bd11-4747-8455-698952b8ce49"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == EchoArgumentsWrapper.TypeId;

		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			EchoArgumentsWrapper wrapper = new(dto);
			
			wrapper.Result = 
			wrapper.EchoArguments(
					wrapper.arguments
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// A command that returns the given arguments
      /// </summary>
		public partial List<ArgumentDTO> EchoArguments(List<ArgumentDTO> arguments);

		/// <summary>Serialize / Deserialize concrete EchoArguments to generic CommandDTO </summary>
		public override CommandDTO Cmd
		{
			get
			{
				CommandDTO cmd = base.Cmd;
				
				this.Set(cmd, "arguments", arguments);
				this.Set(cmd, "Result", Result);
				
				cmd.Response = true;
				return cmd;
			}
			set
			{
				CommandDTO cmd = value;
				
				this.Get(cmd, "arguments",  (()=>this.arguments, x => this.arguments	= x));
				
				base.Cmd = cmd;
				cmd.Response = false;
			}
		}
      /// <summary>The argument list to deserializ</summary>
      public List<ArgumentDTO> arguments { get; set; }
      /// <summary>The serialized list of arguments</summary>

		public List<ArgumentDTO>
		Result { get; set; }
   };


   static partial class ExampleTypes
   {
      public static CommandDTO Dispatch(CommandDTO dto)
      {
         if (null == dto)
            return dto;
         
         else if(ExampleWrapper.IsForMe(dto))
            return ExampleWrapper.ExecuteCommand(dto);
         
         else if(EchoValueWrapper.IsForMe(dto))
            return EchoValueWrapper.ExecuteCommand(dto);
         
         else if(EchoArgumentsWrapper.IsForMe(dto))
            return EchoArgumentsWrapper.ExecuteCommand(dto);
         
         return null;
      }
   }

	///<summary>Types from this module</summary>
   static partial class ExampleTypes
   {
		///<summary>List of cref="CommandTypeDTO" from this module</summary>
      public static void GetTypes(ref List<CommandTypeDTO> commandTypes)
      {
			commandTypes.AddRange(new CommandTypeDTO[] {
				new() {
					Name = "Example",
					Result = "ExampleDTO",
					Parameters = new List<ParamDTO>()
					{
						new() {
							Name="Id",
							Type="UuId",
						},
					},
					Id = Guid.Parse("a84ca129-1d08-4864-a97d-50639b8055d5"),
					Type = CommandTypeDTO.TypeId,
				},
				new() {
					Name = "EchoValue",
					Result = "ValueDTO",
					Parameters = new List<ParamDTO>()
					{
						new() {
							Name="value",
							Type="ValueDTO",
						},
					},
					Id = Guid.Parse("69c8864e-dc43-48ac-9865-99cac19aa5c0"),
					Type = CommandTypeDTO.TypeId,
				},
				new() {
					Name = "EchoArguments",
					Result = "ArgumentDTO[]",
					Parameters = new List<ParamDTO>()
					{
						new() {
							Name="arguments",
							Type="ArgumentDTO[]",
						},
					},
					Id = Guid.Parse("1de8430f-bd11-4747-8455-698952b8ce49"),
					Type = CommandTypeDTO.TypeId,
				},
			});
      }
   }
}
