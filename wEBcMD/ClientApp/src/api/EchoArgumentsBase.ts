import { Guid } from "guid-typescript";
import { equalsGuid } from "src/utils";
import { ArgumentDTO } from "./ArgumentDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * A  command that returns the given arguments
 */
export class EchoArgumentsBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : EchoArgumentsBase.TypeId)}

   /** 1de8430f-bd11-4747-8455-698952b8ce49 is the Id of EchoArguments type. */
   static get TypeId(): Guid { return Guid.parse("1de8430f-bd11-4747-8455-698952b8ce49"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return equalsGuid(dto.Type, EchoArgumentsBase.TypeId); }

   /** The argument list to deserializ */
   get arguments() : ArgumentDTO[]|undefined {
      let arguments = this.getArgument("arguments");
      if (!arguments)
         return undefined;
      return JSON.parse(arguments) as ArgumentDTO[] ;
   }
   set arguments( val : ArgumentDTO[]) {
      this.setArgument("arguments", JSON.stringify(val));
   }

      /** The serialized list of arguments */
   get Result() : ArgumentDTO[]|undefined {
      let result = this.getArgument("Result");
      if (!result)
         return undefined;
      return JSON.parse(result) as ArgumentDTO[] ;
   }

   /// <summary>Calls the command</summary>
   execute(arguments: ArgumentDTO[]): Promise<ArgumentDTO[]|undefined> {
      this.arguments = arguments;
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new EchoArgumentsBase(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<ArgumentDTO[]>(null);
      });
   }
};