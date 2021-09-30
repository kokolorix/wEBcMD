using System;
using System.Reflection;
using System.Collections.Generic;

namespace wEBcMD
{
   public partial class GetCommandTypesWrapper : CommandWrapper
   {
      /// <summary>
      /// All Command-Typs
      /// </summary>

		public partial List<CommandTypeDTO> GetCommandTypes()
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
			return default;
		}
   };

}
