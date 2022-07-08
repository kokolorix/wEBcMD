using System;
using System.Reflection;
using System.Collections.Generic;

namespace wEBcMD
{
   public partial class EchoArgumentsWrapper : CommandWrapper
   {
      /// <summary>
      /// The very first command we designed
      /// </summary>

		public partial List<ArgumentDTO> EchoArguments(List<ArgumentDTO> arguments)
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
			return arguments;
		}
   };

}
