using System;
using System.Reflection;
using System.Collections.Generic;

namespace wEBcMD
{
   public partial class DeleteAdressWrapper : CommandWrapper
   {
      /// <summary>
      /// Delete the Adress with the given id.
      /// Returns the Adress which was deleted.
      /// </summary>

		public partial AdressDTO DeleteAdress(Guid id)
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
			return default;
		}
   };

}
