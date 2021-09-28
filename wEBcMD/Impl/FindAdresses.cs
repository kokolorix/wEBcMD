using System;
using System.Reflection;

namespace wEBcMD
{
   public partial class FindAdressesWrapper : CommandWrapper
   {
      /// <summary>
      /// Addresses search, with multiple tokens
      /// </summary>

		public partial List<AdressDTO> FindAdresses(String searchText)
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
			return default;
		}
   };

}
