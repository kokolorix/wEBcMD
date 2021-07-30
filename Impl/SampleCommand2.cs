using System;
using System.Reflection;

namespace wEBcMD
{
	public partial class SampleCommand2 : CommandWrapper
	{
		/// <summary>Execute the command</summary>
		public partial CommandDTO ExecuteCommand()
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod().DeclaringType.Name}");
         FirstOne = $"I was here: {MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod()}";
         SecondOne = false;
         Cmd.Response = true;
			return Cmd;
		}
	};

}
