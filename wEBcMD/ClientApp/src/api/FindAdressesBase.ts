import { Guid } from "guid-typescript";
import { equalsGuid } from "src/utils";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * Addresses search, with multiple tokens
 */
export class FindAdressesBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : FindAdressesBase.TypeId)}

   /** 13b2f4da-711a-451e-b435-2c2dc1fbbe4e is the Id of FindAdresses type. */
   static get TypeId(): Guid { return Guid.parse("13b2f4da-711a-451e-b435-2c2dc1fbbe4e"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return equalsGuid(dto.Type, FindAdressesBase.TypeId); }

   /** Search text, can contain several words separated by spaces */
   get SearchText() : string|undefined {
      let searchText = this.getArgument("SearchText");
         return searchText;
   }
   set SearchText( val : string) {
      this.setArgument("SearchText", val);
   }

      /** The result of the search is a list of AddressDTO objects */
   get Result() : AdressDTO[]|undefined {
      let result = this.getArgument("Result");
      if (!result)
         return undefined;
      return JSON.parse(result) as AdressDTO[] ;
   }

   /// <summary>Calls the command</summary>
   execute(searchText: string): Promise<AdressDTO[]|undefined> {
      this.SearchText = searchText;
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new FindAdressesBase(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<AdressDTO[]>(null);
      });
   }
};