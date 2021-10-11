import { Guid} from "guid-typescript";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";
import { CommandTypeDTO } from "./CommandTypeDTO";

/**
 * Get a list of all Command-Types
 */
export class GetCommandTypesBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetCommandTypesBase.TypeId)}

   /** 6dab2a85-0256-421c-8a7a-2337453a3e48 is the Id of GetCommandTypes type. */
   static get TypeId(): Guid { return Guid.parse("6dab2a85-0256-421c-8a7a-2337453a3e48"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return dto.Type === GetCommandTypesBase.TypeId; }

      /** The command type object */
   get Result() : CommandTypeDTO[]{
      let result : string = this.getArgument("Result");
      if (!result)
         return null;
      return JSON.parse(result) as CommandTypeDTO[] ;
   }

   /// <summary>Calls the command</summary>
   execute(): Promise<CommandTypeDTO[]> {
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new GetCommandTypesBase(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<CommandTypeDTO[]>(null);
      });
   }
};
