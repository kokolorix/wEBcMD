import { Guid} from "guid-typescript";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 */
export class GetAdressBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetAdressBase.TypeId)}

   /** c6771f60-a64b-4775-a006-a2bce00b23a4 is the Id of GetAdress type. */
   static get TypeId(): Guid { return Guid.parse("c6771f60-a64b-4775-a006-a2bce00b23a4"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return dto.Type === GetAdressBase.TypeId; }

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

   /** The address found or null if it does not exist */
   get Result() : AdressDTO{
      let result : string = this.getArgument("Result");
      if (!result)
         return null;
      return JSON.parse(result) as AdressDTO ;
   }

   /// <summary>Calls the command</summary>
   execute(id: Guid): Promise<AdressDTO> {
      this.Id = id;
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new GetAdressBase(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<AdressDTO>(null);
      });
   }
};