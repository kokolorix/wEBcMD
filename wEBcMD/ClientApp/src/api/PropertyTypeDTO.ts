import { Guid} from "guid-typescript";

/**
 * Definition of an object property
 */
export class PropertyTypeDTO {

   /** 380571d4-21dd-4e78-a6de-950504198eb3 is the Id of PropertyTypeDTO type. */
   static get TypeId(): Guid { return Guid.parse("380571d4-21dd-4e78-a6de-950504198eb3"); }

   /** Name */
   Name?: string;
   /** Type */
   Type?: string;
};