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
      new public static Guid TypeId { get => System.Guid.Parse("5c7fc88a-b15a-4a4b-b687-e320c44743de"); }
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


   static partial class ExampleTypes
   {
      public static CommandDTO Dispatch(CommandDTO dto)
      {
         if (null == dto)
            return dto;
         
         else if(ExampleWrapper.IsForMe(dto))
            return ExampleWrapper.ExecuteCommand(dto);
         
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
			});
      }
   }
}
