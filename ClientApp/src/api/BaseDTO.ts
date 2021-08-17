import { Guid} from "guid-typescript";

/** BaseDTO */
export class BaseDTO {

	/** 1a81bc99-28c2-4c03-ac5c-a1de4967cc36 is the Id of BaseDTO type. */
	static get TypeId(): Guid { return System.Guid.Parse("1a81bc99-28c2-4c03-ac5c-a1de4967cc36"); }

	/** Id */
	Id?: string;
	/** Type */
	Type?: string;
};