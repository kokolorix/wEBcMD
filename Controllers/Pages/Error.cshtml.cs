using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace wEBcMD.Pages
{
   /// <summary>
   ///
   /// </summary>
   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   public class ErrorModel : PageModel
   {
      private readonly ILogger<ErrorModel> _logger;
      /// <summary>
      /// Initialize the logger
      /// </summary>
      /// <param name="logger"></param>
      public ErrorModel(ILogger<ErrorModel> logger) => _logger = logger;

      /// <summary>
      ///
      /// </summary>
      /// <value></value>
      public string RequestId { get; set; }

      /// <summary>
      ///
      /// </summary>
      /// <returns></returns>
      public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

      /// <summary>
      ///
      /// </summary>
      public void OnGet()
      {
         RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
      }
   }
}
