import { Guid } from "guid-typescript";
import { equalsGuid } from "src/utils";
import { ExampleDTO } from "./ExampleDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * The very first command we designed
 */
export class ExampleBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : ExampleBase.TypeId)}

   /** a84ca129-1d08-4864-a97d-50639b8055d5 is the Id of Example type. */
   static get TypeId(): Guid { return Guid.parse("a84ca129-1d08-4864-a97d-50639b8055d5"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return equalsGuid(dto.Type, ExampleBase.TypeId); }

   /** The Id of the Data-Object */
   get Id() : Guid|undefined {
      let id = this.getArgument("Id");
      if (!id)
         return Guid.parse(Guid.EMPTY);
      return Guid.parse(id);
   }
   set Id( val : Guid) {
      this.setArgument("Id", val.toString());
   }

      /** The Data-Object */
   get Result() : ExampleDTO|undefined {
      let result = this.getArgument("Result");
      if (!result)
         return undefined;
      return JSON.parse(result) as ExampleDTO ;
   }

   /// <summary>Calls the command</summary>
   execute(id: Guid): Promise<ExampleDTO|undefined> {
      this.Id = id;
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new ExampleBase(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<ExampleDTO>(null);
      });
   }
};