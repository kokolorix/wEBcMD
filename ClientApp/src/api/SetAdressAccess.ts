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
      return Guid.parse(this.getArgument("Id"));
   }
   set Id( val : Guid) {
      this.setArgument("Id", val.toString());
   }

   /** The address which should be saved, or null if it should be deleted. */
   get Adress() : AdressDTO{
      return JSON.parse(this.getArgument("Adress")) as AdressDTO ;
   }
   set Adress( val : AdressDTO) {
      this.setArgument("Adress", JSON.stringify(val));
   }

   /** The address stored */
   get Result() : AdressDTO{
      return JSON.parse(this.getArgument("Result")) as AdressDTO ;
   }


   execute(id: Guid, adress: AdressDTO): Promise<AdressDTO> {
      this.Id = id;
      this.Adress = adress;
      return SetAdressAccess._service.executeCommand(this.DTO)
      .then((cmd) => {
         return new SetAdressAccess(cmd).Result
      })
      .catch((e) =>{
         console.log(e);
         return new Promise<AdressDTO>(null);
      });
   }
};
