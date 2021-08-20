import { Guid} from "guid-typescript";
import { BaseDTO } from "./BaseDTO";

/** TypeDTO */
export class TypeDTO extends BaseDTO {

   /** 2c8c1feb-0d04-45d2-bbe7-fe137450412e is the Id of TypeDTO type. */
   static get TypeId(): Guid { return Guid.parse("2c8c1feb-0d04-45d2-bbe7-fe137450412e"); }

};