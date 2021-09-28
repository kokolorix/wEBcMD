using System;
using System.Collections.Generic;

namespace wEBcMD
{
   /// <summary>
   /// </summary>
   public class AdressDTO : BaseDTO
   {
      /// <summary>71959238-5bfd-459b-8fba-e48657d8ff2b is the Id of AdressDTO type.</summary>
      new public static Guid TypeId { get => System.Guid.Parse("71959238-5bfd-459b-8fba-e48657d8ff2b"); }
      /// <summary>First name of the person or name of the company</summary>
      public virtual String Name1 { get; set; }
      /// <summary>Surname of the person or additional name of the company</summary>
      public virtual String Name2 { get; set; }
      /// <summary>Street name</summary>
      public virtual String Adress1 { get; set; }
      /// <summary>Address supplement</summary>
      public virtual String Adress2 { get; set; }
      /// <summary>House no.</summary>
      public virtual String Housenumber { get; set; }
      /// <summary>Village</summary>
      public virtual String City { get; set; }
      /// <summary>Postal code</summary>
      public virtual String Postcode { get; set; }
   };

   /// <summary>
   /// Addresses search, with multiple tokens
   /// </summary>
   public partial class FindAdressesWrapper : CommandWrapper
   {
      /// <summary>Constructor of FindAdressesWrapper</summary>
      public FindAdressesWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>13b2f4da-711a-451e-b435-2c2dc1fbbe4e is the Id of FindAdressesWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("13b2f4da-711a-451e-b435-2c2dc1fbbe4e"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == FindAdressesWrapper.TypeId;

		
		
		
		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			FindAdressesWrapper wrapper = new(dto);
			
			wrapper.Result = 
			wrapper.FindAdresses(
					wrapper.SearchText
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// Addresses search, with multiple tokens
      /// </summary>
		public partial List<AdressDTO> FindAdresses(String searchText);

      /// <summary>Serialize / Deserialize concrete FindAdresses to generic CommandDTO</summary>
      public override CommandDTO Cmd
      {
         get
         {
            CommandDTO cmd = base.Cmd;
      
            this.Set(cmd, "SearchText", SearchText);

            cmd.Response = true;
            return cmd;
         }
         set
         {
            CommandDTO cmd = value;
      
            this.Get(cmd, "SearchText",  (()=>this.SearchText, x => this.SearchText = x));
            

            base.Cmd = cmd;
            cmd.Response = false;
         }
      }
            /// <summary>Search text, can contain several words separated by spaces</summary>
      public String SearchText { get; set; }
      /// <summary>The result of the search is a list of AddressDTO objects</summary>

      public List<AdressDTO> Result { get; set; }
   };

   /// <summary>
   /// </summary>
   public partial class GetAdressWrapper : CommandWrapper
   {
      /// <summary>Constructor of GetAdressWrapper</summary>
      public GetAdressWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>c6771f60-a64b-4775-a006-a2bce00b23a4 is the Id of GetAdressWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("c6771f60-a64b-4775-a006-a2bce00b23a4"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == GetAdressWrapper.TypeId;

		
		
		
		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			GetAdressWrapper wrapper = new(dto);
			
			wrapper.Result = 
			wrapper.GetAdress(
					wrapper.Id
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// </summary>
		public partial AdressDTO GetAdress(Guid id);

      /// <summary>Serialize / Deserialize concrete GetAdress to generic CommandDTO</summary>
      public override CommandDTO Cmd
      {
         get
         {
            CommandDTO cmd = base.Cmd;
      
            this.Set(cmd, "Id", Id);

            cmd.Response = true;
            return cmd;
         }
         set
         {
            CommandDTO cmd = value;
      
            this.Get(cmd, "Id",  (()=>this.Id, x => this.Id = x));
            

            base.Cmd = cmd;
            cmd.Response = false;
         }
      }
            /// <summary>Id</summary>
      public Guid Id { get; set; }
      /// <summary>The address found or null if it does not exist</summary>

      public AdressDTO Result { get; set; }
   };

   /// <summary>
   /// Updates an existing address if Id is specified,
   /// or creates a new one if Id is null.
   /// The updated or newly created address is returned in Result.
   /// </summary>
   public partial class SetAdressWrapper : CommandWrapper
   {
      /// <summary>Constructor of SetAdressWrapper</summary>
      public SetAdressWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>c84bb99b-2d11-4426-87fa-119dc892f4ec is the Id of SetAdressWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("c84bb99b-2d11-4426-87fa-119dc892f4ec"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == SetAdressWrapper.TypeId;

		
		
		
		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			SetAdressWrapper wrapper = new(dto);
			
			wrapper.Result = 
			wrapper.SetAdress(
					wrapper.Id, 
					wrapper.Adress
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// Updates an existing address if Id is specified,
      /// or creates a new one if Id is null.
      /// The updated or newly created address is returned in Result.
      /// </summary>
		public partial AdressDTO SetAdress(Guid id, AdressDTO adress);

      /// <summary>Serialize / Deserialize concrete SetAdress to generic CommandDTO</summary>
      public override CommandDTO Cmd
      {
         get
         {
            CommandDTO cmd = base.Cmd;
      
            this.Set(cmd, "Id", Id);
            this.Set(cmd, "Adress", Adress);

            cmd.Response = true;
            return cmd;
         }
         set
         {
            CommandDTO cmd = value;
      
            this.Get(cmd, "Id",  (()=>this.Id, x => this.Id = x));
            this.Get(cmd, "Adress",  (()=>this.Adress, x => this.Adress = x));
            

            base.Cmd = cmd;
            cmd.Response = false;
         }
      }
            /// <summary>Id</summary>
      public Guid Id { get; set; }
      /// <summary>The address which should be saved, or null if it should be deleted.</summary>
      public AdressDTO Adress { get; set; }
      /// <summary>The address stored</summary>

      public AdressDTO Result { get; set; }
   };

   /// <summary>
   /// Delete the Adress with the given id.
   /// Returns the Adress which was deleted.
   /// </summary>
   public partial class DeleteAdressWrapper : CommandWrapper
   {
      /// <summary>Constructor of DeleteAdressWrapper</summary>
      public DeleteAdressWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>c60a9e66-b60a-4228-a7e5-ca61285ce5de is the Id of DeleteAdressWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("c60a9e66-b60a-4228-a7e5-ca61285ce5de"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == DeleteAdressWrapper.TypeId;

		
		
		
		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			DeleteAdressWrapper wrapper = new(dto);
			
			wrapper.Result = 
			wrapper.DeleteAdress(
					wrapper.Id
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// Delete the Adress with the given id.
      /// Returns the Adress which was deleted.
      /// </summary>
		public partial AdressDTO DeleteAdress(Guid id);

      /// <summary>Serialize / Deserialize concrete DeleteAdress to generic CommandDTO</summary>
      public override CommandDTO Cmd
      {
         get
         {
            CommandDTO cmd = base.Cmd;
      
            this.Set(cmd, "Id", Id);

            cmd.Response = true;
            return cmd;
         }
         set
         {
            CommandDTO cmd = value;
      
            this.Get(cmd, "Id",  (()=>this.Id, x => this.Id = x));
            

            base.Cmd = cmd;
            cmd.Response = false;
         }
      }
            /// <summary>Id</summary>
      public Guid Id { get; set; }
      /// <summary>The deleted address</summary>

      public AdressDTO Result { get; set; }
   };


   static class AdressTypesDispatcher
   {
      public static CommandDTO Dispatch(CommandDTO dto)
      {
         if (null == dto)
            return dto;
         
         else if(FindAdressesWrapper.IsForMe(dto))
            return FindAdressesWrapper.ExecuteCommand(dto);
         
         else if(GetAdressWrapper.IsForMe(dto))
            return GetAdressWrapper.ExecuteCommand(dto);
         
         else if(SetAdressWrapper.IsForMe(dto))
            return SetAdressWrapper.ExecuteCommand(dto);
         
         else if(DeleteAdressWrapper.IsForMe(dto))
            return DeleteAdressWrapper.ExecuteCommand(dto);
         
         return null;
      }
   }
}
