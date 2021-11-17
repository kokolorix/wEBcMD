import { Guid} from "guid-typescript";
import { TypeDTO } from "./TypeDTO";

/**
 */
export class ObjectTypeDTO extends TypeDTO {

   /** 9b286c28-4baf-44dd-9328-9729854f4882 is the Id of ObjectTypeDTO type. */
   static get TypeId(): Guid { return Guid.parse("9b286c28-4baf-44dd-9328-9729854f4882"); }

};