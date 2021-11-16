import { Guid} from "guid-typescript";

/** ParamDTO */
export class ParamDTO {

   /** 95f50f9b-9e6c-4f72-8bfe-486adffda5a9 is the Id of ParamDTO type. */
   static get TypeId(): Guid { return Guid.parse("95f50f9b-9e6c-4f72-8bfe-486adffda5a9"); }

   /** Name */
   Name?: string;
   /** Type */
   Type?: string;
};