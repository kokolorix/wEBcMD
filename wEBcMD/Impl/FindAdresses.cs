using System;
using System.Reflection;
using System.Collections.Generic;

namespace wEBcMD
{
   public partial class FindAdressesWrapper : CommandWrapper
   {
      /// <summary>Execute the command</summary>
      public partial CommandDTO ExecuteCommand()
      {
         Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
         return Cmd;
      }
      /// <summary>
      /// Addresses search, with multiple tokens
      /// </summary>

		public List<AdressDTO> FindAdresses(String searchText)
		{
			return default;
		}
   };

}
