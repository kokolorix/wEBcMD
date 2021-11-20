import { Guid} from "guid-typescript";
import { Enum=String,Boolean,Double,Int32,*DTO } from "./Enum=String,Boolean,Double,Int32,*DTO";

/** DatatypeDTO */
export class DatatypeDTO {

   /** 91d670c8-dedd-41c9-8407-7532b1fad45a is the Id of DatatypeDTO type. */
   static get TypeId(): Guid { return Guid.parse("91d670c8-dedd-41c9-8407-7532b1fad45a"); }

   /** Type */
   Type?: Enum=String,Boolean,Double,Int32,*DTO;
};