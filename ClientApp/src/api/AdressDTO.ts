import { Guid} from "guid-typescript";
import { BaseDTO } from "./BaseDTO";

/**
 */
export class AdressDTO extends BaseDTO {

   /** 71959238-5bfd-459b-8fba-e48657d8ff2b is the Id of AdressDTO type. */
   static get TypeId(): Guid { return Guid.parse("71959238-5bfd-459b-8fba-e48657d8ff2b"); }

   /** First name of the person or name of the company */
   Name1?: string;
   /** Surname of the person or additional name of the company */
   Name2?: string;
   /** Street name */
   Adress1?: string;
   /** Address supplement */
   Adress2?: string;
   /** House no. */
   Housenumber?: string;
   /** Village */
   City?: string;
   /** Postal code */
   Postcode?: string;
};