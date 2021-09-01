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
   public partial class FindAdresses : CommandWrapper
   {
      /// <summary>Constructor of FindAdresses</summary>
      public FindAdresses(CommandDTO dto = null):base(dto){
         _result = new(Cmd.Arguments);
      }
      /// <summary>13b2f4da-711a-451e-b435-2c2dc1fbbe4e is the Id of FindAdresses type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("13b2f4da-711a-451e-b435-2c2dc1fbbe4e"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == FindAdresses.TypeId;
      /// <summary>Create the wrapper and execute the command</summary>
      public static CommandDTO ExecuteCommand(CommandDTO dto) => new FindAdresses(dto).ExecuteCommand();
      /// <summary>Execute the command</summary>
      public partial CommandDTO ExecuteCommand();
      /// <summary>Search text, can contain several words separated by spaces</summary>
      public String SearchText {
         get => this._string["SearchText"];
         set => this._string["SearchText"] = value;
      }
      /// <summary> access helper for Result</summary>
      protected DTOValues<List<AdressDTO>> _result;
      /// <summary>The result of the search is a list of AddressDTO objects</summary>
      public List<AdressDTO> Result {
         get => this._result["Result"];
         set => this._result["Result"] = value;
      }
   };

   /// <summary>
   /// </summary>
   public partial class GetAdress : CommandWrapper
   {
      /// <summary>Constructor of GetAdress</summary>
      public GetAdress(CommandDTO dto = null):base(dto){
         _result = new(Cmd.Arguments);
      }
      /// <summary>c6771f60-a64b-4775-a006-a2bce00b23a4 is the Id of GetAdress type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("c6771f60-a64b-4775-a006-a2bce00b23a4"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == GetAdress.TypeId;
      /// <summary>Create the wrapper and execute the command</summary>
      public static CommandDTO ExecuteCommand(CommandDTO dto) => new GetAdress(dto).ExecuteCommand();
      /// <summary>Execute the command</summary>
      public partial CommandDTO ExecuteCommand();
      /// <summary>Id</summary>
      public Guid Id {
         get => this._guid["Id"];
         set => this._guid["Id"] = value;
      }
      /// <summary> access helper for Result</summary>
      protected DTOValues<AdressDTO> _result;
      /// <summary>The address found or null if it does not exist</summary>
      public AdressDTO Result {
         get => this._result["Result"];
         set => this._result["Result"] = value;
      }
   };

   /// <summary>
   /// Updates an existing address if Id is specified,
   /// or creates a new one if Id is null.
   /// The updated or newly created address is returned in Result.
   /// </summary>
   public partial class SetAdress : CommandWrapper
   {
      /// <summary>Constructor of SetAdress</summary>
      public SetAdress(CommandDTO dto = null):base(dto){
         _adress = new(Cmd.Arguments);
         _result = new(Cmd.Arguments);
      }
      /// <summary>c84bb99b-2d11-4426-87fa-119dc892f4ec is the Id of SetAdress type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("c84bb99b-2d11-4426-87fa-119dc892f4ec"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == SetAdress.TypeId;
      /// <summary>Create the wrapper and execute the command</summary>
      public static CommandDTO ExecuteCommand(CommandDTO dto) => new SetAdress(dto).ExecuteCommand();
      /// <summary>Execute the command</summary>
      public partial CommandDTO ExecuteCommand();
      /// <summary>Id</summary>
      public Guid Id {
         get => this._guid["Id"];
         set => this._guid["Id"] = value;
      }
      /// <summary> access helper for Adress</summary>
      protected DTOValues<AdressDTO> _adress;
      /// <summary>The address which should be saved, or null if it should be deleted.</summary>
      public AdressDTO Adress {
         get => this._adress["Adress"];
         set => this._adress["Adress"] = value;
      }
      /// <summary> access helper for Result</summary>
      protected DTOValues<AdressDTO> _result;
      /// <summary>The address stored</summary>
      public AdressDTO Result {
         get => this._result["Result"];
         set => this._result["Result"] = value;
      }
   };


   static class AdressTypesDispatcher
   {
      public static CommandDTO Dispatch(CommandDTO dto)
      {
         if (null == dto)
            return dto;
         
         else if(FindAdresses.IsForMe(dto))
            return FindAdresses.ExecuteCommand(dto);
         
         else if(GetAdress.IsForMe(dto))
            return GetAdress.ExecuteCommand(dto);
         
         else if(SetAdress.IsForMe(dto))
            return SetAdress.ExecuteCommand(dto);
         
         return null;
      }
   }
}
