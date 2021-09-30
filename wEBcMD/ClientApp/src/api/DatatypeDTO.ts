import { Guid} from "guid-typescript";
import { Enum=String,Boolean,Double,Int32,*DTO } from "./Enum=String,Boolean,Double,Int32,*DTO";

/** DatatypeDTO */
export class DatatypeDTO {

   /** d02743ec-83f6-4209-a940-daa07131716e is the Id of DatatypeDTO type. */
   static get TypeId(): Guid { return Guid.parse("d02743ec-83f6-4209-a940-daa07131716e"); }

   /** Type */
   Type?: Enum=String,Boolean,Double,Int32,*DTO;
};