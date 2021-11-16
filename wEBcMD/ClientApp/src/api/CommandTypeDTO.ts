import { Guid} from "guid-typescript";
import { ParamDTO } from "./ParamDTO";
import { TypeDTO } from "./TypeDTO";

/** CommandTypeDTO */
export class CommandTypeDTO extends TypeDTO {

   /** 7e4e81c9-9170-4f9e-bfe0-b9acd359958b is the Id of CommandTypeDTO type. */
   static get TypeId(): Guid { return Guid.parse("7e4e81c9-9170-4f9e-bfe0-b9acd359958b"); }

   /** Name */
   Name?: string;
   /** Result */
   Result?: string;
   /** Parameters */
   Parameters?: ParamDTO[];
};