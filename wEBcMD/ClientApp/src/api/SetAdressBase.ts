import { Guid } from "guid-typescript";
import { equalsGuid } from "src/utils";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * Updates an existing address if Id is specified,
 * or creates a new one if Id is null.
 * The updated or newly created address is returned in Result.
 */
export class SetAdressBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : SetAdressBase.TypeId)}

   /** c84bb99b-2d11-4426-87fa-119dc892f4ec is the Id of SetAdress type. */
   static get TypeId(): Guid { return Guid.parse("c84bb99b-2d11-4426-87fa-119dc892f4ec"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return equalsGuid(dto.Type, SetAdressBase.TypeId); }

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

   /** The address which should be saved, or null if it should be deleted. */
   get Adress() : AdressDTO|undefined {
      let adress = this.getArgument("Adress");
      if (!adress)
         return undefined;
      return JSON.parse(adress) as AdressDTO ;
   }
   set Adress( val : AdressDTO) {
      this.setArgument("Adress", JSON.stringify(val));
   }

      /** The address stored */
   get Result() : AdressDTO|undefined {
      let result = this.getArgument("Result");
      if (!result)
         return undefined;
      return JSON.parse(result) as AdressDTO ;
   }

   /// <summary>Calls the command</summary>
   execute(id: Guid, adress: AdressDTO): Promise<AdressDTO|undefined> {
      this.Id = id;
      this.Adress = adress;
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new SetAdressBase(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<AdressDTO>(null);
      });
   }
};