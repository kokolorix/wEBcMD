using System;
using System.Reflection;

namespace wEBcMD
{
   public partial class SetAdressWrapper : CommandWrapper
   {
      // public partial CommandDTO ExecuteCommand()
      // {
      //    this.Result = this.Adress;
      //    this.Adress = null;
      //    this.Result.Id = System.Guid.NewGuid();
      //    this.Result.Type = AdressDTO.TypeId;

      //    return Cmd;
      // }
		/// <summary>
		///  Execute the command
		/// </summary>
		/// <param name="id"></param>
		/// <param name="adress"></param>
		/// <returns></returns>
		public partial AdressDTO SetAdress(Guid id, AdressDTO adress)
		{
         Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
         return default;
		}
	};


}
