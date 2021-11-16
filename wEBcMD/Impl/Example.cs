using System;
using System.Reflection;
using System.Collections.Generic;

namespace wEBcMD
{
   public partial class ExampleWrapper : CommandWrapper
   {
      /// <summary>
      /// The very first command we designed
      /// </summary>

		public partial ExampleDTO Example(Guid id)
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
			return default;
		}
   };

}
