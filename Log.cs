using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wEBcMD
{
	public class Log
	{
		public static void Trace(ILogger logger, string message,
		[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
		[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
		[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
			System.Diagnostics.Trace.WriteLine(msg);
			logger.Log(LogLevel.Trace, msg);
			Console.WriteLine(msg);
		}
		public static void Info(ILogger logger, string message,
		[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
		[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
		[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
			System.Diagnostics.Trace.TraceInformation(msg);
			logger.Log(LogLevel.Information, msg);
			Console.WriteLine(msg);
		}
		public static void Warn(ILogger logger, string message,
		[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
		[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
		[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
			System.Diagnostics.Trace.TraceWarning(msg);
			logger.Log(LogLevel.Warning, msg);
			Console.WriteLine(msg);
		}
		public static void Error(ILogger logger, string message,
		[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
		[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
		[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
		{
			string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
			System.Diagnostics.Trace.TraceError(msg);
			logger.Log(LogLevel.Error, msg);
			Console.WriteLine(msg);
		}
	}
}
