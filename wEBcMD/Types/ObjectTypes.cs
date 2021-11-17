using System;
using System.Collections.Generic;

namespace wEBcMD
{
   /// <summary>
   /// </summary>
   public class ObjectTypeDTO : TypeDTO
   {
      /// <summary>9b286c28-4baf-44dd-9328-9729854f4882 is the Id of ObjectTypeDTO type.</summary>
      new public static Guid TypeId { get => System.Guid.Parse("9b286c28-4baf-44dd-9328-9729854f4882"); }
   };

   /// <summary>
   /// </summary>
   public partial class GetObjectTypesWrapper : CommandWrapper
   {
      /// <summary>Constructor of GetObjectTypesWrapper</summary>
      public GetObjectTypesWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>058aa634-956c-4167-b5e0-2c2171ae55c0 is the Id of GetObjectTypesWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("058aa634-956c-4167-b5e0-2c2171ae55c0"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == GetObjectTypesWrapper.TypeId;

		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			GetObjectTypesWrapper wrapper = new(dto);
			
			wrapper.Result = 
			wrapper.GetObjectTypes(
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// </summary>
		public partial List<ObjectTypeDTO> GetObjectTypes();

		/// <summary>Serialize / Deserialize concrete GetObjectTypes to generic CommandDTO </summary>
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
      /// <summary></summary>

		public List<ObjectTypeDTO>
		Result { get; set; }
   };


   static partial class ObjectTypes
   {
      public static CommandDTO Dispatch(CommandDTO dto)
      {
         if (null == dto)
            return dto;
         
         else if(GetObjectTypesWrapper.IsForMe(dto))
            return GetObjectTypesWrapper.ExecuteCommand(dto);
         
         return null;
      }
   }

	///<summary>Types from this module</summary>
   static partial class ObjectTypes
   {
		///<summary>List of cref="CommandTypeDTO" from this module</summary>
      public static void GetTypes(ref List<CommandTypeDTO> commandTypes)
      {
			commandTypes.AddRange(new CommandTypeDTO[] {
				new() {
					Name = "GetObjectTypes",
					Result = "ObjectTypeDTO[]",
					Parameters = new List<ParamDTO>()
					{
						
					},
					Id = Guid.Parse("058aa634-956c-4167-b5e0-2c2171ae55c0"),
					Type = CommandTypeDTO.TypeId,
				},
			});
      }
   }
}
