using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wEBcMD
{
   /// <summary>
   /// Logging helpers
   /// </summary>
	public class Log
   {
      /// <summary>
      /// Log Traces
      /// </summary>
      /// <param name="message"></param>
      /// <param name="memberName"></param>
      /// <param name="sourceFilePath"></param>
      /// <param name="sourceLineNumber"></param>
		public static void Trace(string message = null,
      [System.Runtime.CompilerServices.CallerMemberName] string memberName = null,
      [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = null,
      [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
      {
         string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
         System.Diagnostics.Trace.WriteLine(msg);
         Console.WriteLine(msg);
      }
      /// <summary>
      /// Log Traces
      /// </summary>
      /// <param name="logger"></param>
      /// <param name="message"></param>
      /// <param name="memberName"></param>
      /// <param name="sourceFilePath"></param>
      /// <param name="sourceLineNumber"></param>
		public static void Trace(ILogger logger, string message = null,
      [System.Runtime.CompilerServices.CallerMemberName] string memberName = null,
      [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = null,
      [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
      {
         string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
         System.Diagnostics.Trace.WriteLine(msg);
         logger.Log(LogLevel.Trace, msg);
         Console.WriteLine(msg);
      }
      /// <summary>
      /// Log Infos
      /// </summary>
      /// <param name="logger"></param>
      /// <param name="message"></param>
      /// <param name="memberName"></param>
      /// <param name="sourceFilePath"></param>
      /// <param name="sourceLineNumber"></param>
      public static void Info(ILogger logger, string message,
[System.Runtime.CompilerServices.CallerMemberName] string memberName = null,
[System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = null,
[System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
      {
         string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
         System.Diagnostics.Trace.TraceInformation(msg);
         logger.Log(LogLevel.Information, msg);
         Console.WriteLine(msg);
      }

      /// <summary>
      /// Log warnings
      /// </summary>
      /// <param name="logger"></param>
      /// <param name="message"></param>
      /// <param name="memberName"></param>
      /// <param name="sourceFilePath"></param>
      /// <param name="sourceLineNumber"></param>
      public static void Warn(ILogger logger, string message = null,
      [System.Runtime.CompilerServices.CallerMemberName] string memberName = null,
      [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = null,
      [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
      {
         string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
         System.Diagnostics.Trace.TraceWarning(msg);
         logger.Log(LogLevel.Warning, msg);
         Console.WriteLine(msg);
      }

      /// <summary>
      /// Log errors
      /// </summary>
      /// <param name="logger"></param>
      /// <param name="message"></param>
      /// <param name="memberName"></param>
      /// <param name="sourceFilePath"></param>
      /// <param name="sourceLineNumber"></param>
      public static void Error(ILogger logger, string message,
      [System.Runtime.CompilerServices.CallerMemberName] string memberName = null,
      [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = null,
      [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
      {
         string msg = $"{sourceFilePath}({sourceLineNumber})	{message}	{memberName}";
         System.Diagnostics.Trace.TraceError(msg);
         logger.Log(LogLevel.Error, msg);
         Console.WriteLine(msg);
      }
   }
}
