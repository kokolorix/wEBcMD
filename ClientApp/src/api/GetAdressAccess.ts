import { Guid} from "guid-typescript";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 */
export class GetAdressAccess  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetAdressAccess.TypeId)}

   /** c6771f60-a64b-4775-a006-a2bce00b23a4 is the Id of GetAdress type. */
   static get TypeId(): Guid { return Guid.parse("c6771f60-a64b-4775-a006-a2bce00b23a4"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return Guid.parse(dto.Type) === GetAdressAccess.TypeId; }

   /** Id */
   get Id() : Guid{
      return Guid.parse(this.getArgument("Id"));
   }
   set Id( val : Guid) {
      this.setArgument("Id", val.toString());
   }

   /** The address found or null if it does not exist */
   get Result() : AdressDTO{
      return JSON.parse(this.getArgument("Result")) as AdressDTO ;
   }

         
   /// <summary>Calls the command</summary>
   execute(id: Guid): Promise<AdressDTO> {
      this.Id = id;
      return GetAdressAccess._service.executeCommand(this.DTO)
      .then((cmd) => {
         return new GetAdressAccess(cmd).Result
      })
      .catch((e) =>{
         console.log(e);
         return new Promise<AdressDTO>(null);
      });
   }
};
      