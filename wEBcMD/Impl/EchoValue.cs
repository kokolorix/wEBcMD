using System;
using System.Reflection;
using System.Collections.Generic;

namespace wEBcMD
{
   public partial class EchoValueWrapper : CommandWrapper
   {
      /// <summary>
      /// The very first command we designed
      /// </summary>

		public partial ValueDTO EchoValue(ValueDTO value)
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
			return value;
		}
   };

}
