using System;
using System.Reflection;

namespace wEBcMD
{
   public partial class SetAdressWrapper : CommandWrapper
   {
      /// <summary>Execute the command</summary>
      // public partial CommandDTO ExecuteCommand()
      // {
      //    Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
      //    this.Result = this.Adress;
      //    this.Adress = null;
      //    this.Result.Id = System.Guid.NewGuid();
      //    this.Result.Type = AdressDTO.TypeId;

      //    return Cmd;
      // }
		/// <summary>
		///
		/// </summary>
		/// <param name="id"></param>
		/// <param name="adress"></param>
		/// <returns></returns>
		public partial AdressDTO SetAdress(Guid id, AdressDTO adress)
		{
			return default;
		}
	};


}
