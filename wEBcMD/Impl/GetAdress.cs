using System;
using System.Reflection;
using System.Collections.Generic;

namespace wEBcMD
{
   public partial class GetAdressWrapper : CommandWrapper
   {
      /// <summary>
      /// </summary>

		public partial AdressDTO GetAdress(Guid id)
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
			return default;
		}
   };

}
