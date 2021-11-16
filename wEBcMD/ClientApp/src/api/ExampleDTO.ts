import { Guid} from "guid-typescript";
import { BaseDTO } from "./BaseDTO";

/**
 * Description of the Data-Strucure.
 */
export class ExampleDTO extends BaseDTO {

   /** 5c7fc88a-b15a-4a4b-b687-e320c44743de is the Id of ExampleDTO type. */
   static get TypeId(): Guid { return Guid.parse("5c7fc88a-b15a-4a4b-b687-e320c44743de"); }

   /** First property, a string */
   One?: string;
   /** Second one, a boolean */
   Two?: boolean;
};