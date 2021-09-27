
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace wEBcMD.Controllers
{
               /// <summary>
   /// 
   /// Types and Commands around Adresses.
   /// 
   /// </summary>

   [ApiController]
   [Route("[controller]")]
   public class AdressTypesController : ControllerBase
   {
      private readonly ILogger<AdressTypesController> _logger;
      /// <summary>
      /// Initialize the logger
      /// </summary>
      public AdressTypesController(ILogger<AdressTypesController> logger) => _logger = logger;

      /// <summary>
      /// Addresses search, with multiple tokens
      /// </summary>

      [HttpGet]
      [Route("findadresses")]
      
      public List<AdressDTO> FindAdresses( String searchText )
      {
         FindAdressesWrapper wrapper = new();
         
         wrapper.SearchText = searchText;

         wrapper.ExecuteCommand();

         return wrapper.Result;
      }
         /// <summary>
      /// </summary>

      [HttpGet]
      [Route("getadress")]
      
      public AdressDTO GetAdress( Guid id )
      {
         GetAdressWrapper wrapper = new();
         
         wrapper.Id = id;

         wrapper.ExecuteCommand();

         return wrapper.Result;
      }
         /// <summary>
      /// Updates an existing address if Id is specified,
      /// or creates a new one if Id is null.
      /// The updated or newly created address is returned in Result.
      /// </summary>

      [HttpPost]
      [Route("setadress")]
      
      public AdressDTO SetAdress( Guid id, AdressDTO adress )
      {
         SetAdressWrapper wrapper = new();
         
         wrapper.Id = id;
         wrapper.Adress = adress;

         wrapper.ExecuteCommand();

         return wrapper.Result;
      }
         /// <summary>
      /// Delete the Adress with the given id.
      /// Returns the Adress which was deleted.
      /// </summary>

      [HttpDelete]
      [Route("deleteadress")]
      
      public AdressDTO DeleteAdress( Guid id )
      {
         DeleteAdressWrapper wrapper = new();
         
         wrapper.Id = id;

         wrapper.ExecuteCommand();

         return wrapper.Result;
      }
   
   }

}
         