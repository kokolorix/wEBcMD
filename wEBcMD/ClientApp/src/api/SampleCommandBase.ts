import { Guid} from "guid-typescript";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * This is the sample command. He has two Parameters
 * and a multiline summary.
 * ``` typescript
 * CommandDTO cmd;
 * if(SampleCommand.IsForMe(dto)){
 *    let sample = new SampleCommand(cmd);
 *    console.log(sample.FirstOne);
 * }
 * ```
 */
export class SampleCommandBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : SampleCommandBase.TypeId)}

   /** e3e185bd-5237-4574-977f-a040bbe12d35 is the Id of SampleCommand type. */
   static get TypeId(): Guid { return Guid.parse("e3e185bd-5237-4574-977f-a040bbe12d35"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return Guid.parse(dto.Type) === SampleCommandBase.TypeId; }

   /**
    * The FirstOne is a string parameter
    * and has a multiline comment
    */
   get FirstOne() : string{
      let firstOne : string = this.getArgument("FirstOne");
         return firstOne;
   }
   set FirstOne( val : string) {
      this.setArgument("FirstOne", val);
   }

   /** The SecondOne is a boolean parameter */
   get SecondOne() : boolean{
      let secondOne : string = this.getArgument("SecondOne");
      if (!secondOne)
         return false;
      return Boolean(JSON.parse(secondOne));
   }
   set SecondOne( val : boolean) {
      this.setArgument("SecondOne", val.toString());
   }


   /// <summary>Calls the command</summary>
   execute(firstOne: string, secondOne: boolean): Promise<void> {
      this.FirstOne = firstOne;
      this.SecondOne = secondOne;
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<void>(null);
      });
   }
};