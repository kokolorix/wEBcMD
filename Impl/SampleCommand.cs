using System;
using System.Reflection;

namespace wEBcMD
{
   public partial class SampleCommand : CommandWrapper
   {
      /// <summary>Execute the command</summary>
      public partial CommandDTO ExecuteCommand()
      {
         Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
         return Cmd;
      }
   };

}
