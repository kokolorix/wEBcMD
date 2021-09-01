import { Guid} from "guid-typescript";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * Updates an existing address if Id is specified,
 * or creates a new one if Id is null.
 * The updated or newly created address is returned in Result.
 */
export class SetAdressAccess  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : SetAdressAccess.TypeId)}

   /** c84bb99b-2d11-4426-87fa-119dc892f4ec is the Id of SetAdress type. */
   static get TypeId(): Guid { return Guid.parse("c84bb99b-2d11-4426-87fa-119dc892f4ec"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return Guid.parse(dto.Type) === SetAdressAccess.TypeId; }

   /** Id */
   get Id() : Guid{
      let id : string = this.getArgument("Id");
      if (!id)
         return Guid.parse(Guid.EMPTY);
      return Guid.parse(id);
   }
   set Id( val : Guid) {
      this.setArgument("Id", val.toString());
   }

   /** The address which should be saved, or null if it should be deleted. */
   get Adress() : AdressDTO{
      let adress : string = this.getArgument("Adress");
      if (!adress)
         return null;
      return JSON.parse(adress) as AdressDTO ;
   }
   set Adress( val : AdressDTO) {
      this.setArgument("Adress", JSON.stringify(val));
   }

   /** The address stored */
   get Result() : AdressDTO{
      let result : string = this.getArgument("Result");
      if (!result)
         return null;
      return JSON.parse(result) as AdressDTO ;
   }

   /// <summary>Calls the command</summary>
   execute(id: Guid, adress: AdressDTO): Promise<AdressDTO> {
      this.Id = id;
      this.Adress = adress;
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new SetAdressAccess(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<AdressDTO>(null);
      });
   }
};