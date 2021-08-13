using System;
using System.Reflection;

namespace wEBcMD
{
	public partial class SampleCommand2 : CommandWrapper
	{
		/// <summary>Execute the command</summary>
		public partial CommandDTO ExecuteCommand()
		{
			Log.Trace($"Implementation in {MethodBase.GetCurrentMethod()}");
			return Cmd;
		}
	};

}
