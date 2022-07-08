import { Guid } from "guid-typescript";
import { equalsGuid } from "src/utils";
import { ValueDTO } from "./ValueDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * A command, that returns the given value
 */
export class EchoValueBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : EchoValueBase.TypeId)}

   /** 69c8864e-dc43-48ac-9865-99cac19aa5c0 is the Id of EchoValue type. */
   static get TypeId(): Guid { return Guid.parse("69c8864e-dc43-48ac-9865-99cac19aa5c0"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return equalsGuid(dto.Type, EchoValueBase.TypeId); }

   /** The value to deserialize */
   get value() : ValueDTO|undefined {
      let value = this.getArgument("value");
      if (!value)
         return undefined;
      return JSON.parse(value) as ValueDTO ;
   }
   set value( val : ValueDTO) {
      this.setArgument("value", JSON.stringify(val));
   }

      /** The serialized value */
   get Result() : ValueDTO|undefined {
      let result = this.getArgument("Result");
      if (!result)
         return undefined;
      return JSON.parse(result) as ValueDTO ;
   }

   /// <summary>Calls the command</summary>
   execute(value: ValueDTO): Promise<ValueDTO|undefined> {
      this.value = value;
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new EchoValueBase(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<ValueDTO>(null);
      });
   }
};