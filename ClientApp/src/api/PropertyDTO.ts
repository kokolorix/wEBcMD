import { Guid} from "guid-typescript";

/** PropertyDTO */
export class PropertyDTO {

	/** 24ef4fd2-e337-4baa-89dd-404a72200871 is the Id of PropertyDTO type. */
	static get TypeId(): Guid { return Guid.parse("24ef4fd2-e337-4baa-89dd-404a72200871"); }

	/** Name */
	Name?: string;
	/** Value */
	Value?: string;
};