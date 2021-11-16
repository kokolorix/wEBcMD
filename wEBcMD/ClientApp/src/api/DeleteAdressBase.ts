import { Guid } from "guid-typescript";
import { equalsGuid } from "src/utils";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * Delete the Adress with the given id.
 *          Returns the Adress which was deleted.
 */
export class DeleteAdressBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : DeleteAdressBase.TypeId)}

   /** c60a9e66-b60a-4228-a7e5-ca61285ce5de is the Id of DeleteAdress type. */
   static get TypeId(): Guid { return Guid.parse("c60a9e66-b60a-4228-a7e5-ca61285ce5de"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return equalsGuid(dto.Type, DeleteAdressBase.TypeId); }

   /** Id */
   get Id() : Guid|undefined {
      let id = this.getArgument("Id");
      if (!id)
         return Guid.parse(Guid.EMPTY);
      return Guid.parse(id);
   }
   set Id( val : Guid) {
      this.setArgument("Id", val.toString());
   }

      /** The deleted address */
   get Result() : AdressDTO|undefined {
      let result = this.getArgument("Result");
      if (!result)
         return undefined;
      return JSON.parse(result) as AdressDTO ;
   }

   /// <summary>Calls the command</summary>
   execute(id: Guid): Promise<AdressDTO|undefined> {
      this.Id = id;
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new DeleteAdressBase(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<AdressDTO>(null);
      });
   }
};