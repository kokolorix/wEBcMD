using System;
using System.Collections.Generic;

namespace wEBcMD
{
   /// <summary>BaseDTO</summary>
   public partial class BaseDTO
   {
      /// <summary>1a81bc99-28c2-4c03-ac5c-a1de4967cc36 is the Id of BaseDTO type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("1a81bc99-28c2-4c03-ac5c-a1de4967cc36"); }
      /// <summary>Id</summary>
      public virtual Guid Id { get; set; }
      /// <summary>Type</summary>
      public virtual Guid Type { get; set; }
   };

   /// <summary>PropertyDTO</summary>
   public class PropertyDTO
   {
      /// <summary>24ef4fd2-e337-4baa-89dd-404a72200871 is the Id of PropertyDTO type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("24ef4fd2-e337-4baa-89dd-404a72200871"); }
      /// <summary>Name</summary>
      public virtual String Name { get; set; }
      /// <summary>Value</summary>
      public virtual ValueDTO Value { get; set; }
   };

   /// <summary>ObjectDTO</summary>
   public class ObjectDTO : BaseDTO
   {
      /// <summary>1a81bc99-28c2-4c03-ac5c-a1de4967cc36 is the Id of ObjectDTO type.</summary>
      public new static Guid TypeId { get => System.Guid.Parse("1a81bc99-28c2-4c03-ac5c-a1de4967cc36"); }
      /// <summary>Properties</summary>
      public virtual List<PropertyDTO> Properties { get; set; }
   };

   /// <summary>TypeDTO</summary>
   public class TypeDTO : BaseDTO
   {
      /// <summary>2c8c1feb-0d04-45d2-bbe7-fe137450412e is the Id of TypeDTO type.</summary>
      public new static Guid TypeId { get => System.Guid.Parse("2c8c1feb-0d04-45d2-bbe7-fe137450412e"); }
   };


   static partial class BaseTypes
   {
      public static CommandDTO Dispatch(CommandDTO dto)
      {
         if (null == dto)
            return dto;
         
         return null;
      }
   }

	///<summary>Types from this module</summary>
   static partial class BaseTypes
   {
		///<summary>List of cref="CommandTypeDTO" from this module</summary>
      public static void GetTypes(ref List<CommandTypeDTO> commandTypes)
      {
			commandTypes.AddRange(new CommandTypeDTO[] {
			});
      }
   }
}
