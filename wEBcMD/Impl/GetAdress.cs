using System;
using System.Reflection;

namespace wEBcMD
{
   public partial class GetAdress : CommandWrapper
   {
      /// <summary>Execute the command</summary>
      public partial CommandDTO ExecuteCommand()
      {
         Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
         return Cmd;
      }
   };

}
