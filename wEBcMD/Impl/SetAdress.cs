using System;
using System.Reflection;

namespace wEBcMD
{
   public partial class SetAdressWrapper : CommandWrapper
   {
      /// <summary>Execute the command</summary>
      public partial CommandDTO ExecuteCommand()
      {
         Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
         this.Result = this.Adress;
         this.Adress = null;
         this.Result.Id = System.Guid.NewGuid();
         this.Result.Type = AdressDTO.TypeId;

         return Cmd;
      }
   };

}
