import { Guid} from "guid-typescript";
import { PropertyDTO } from "./PropertyDTO";
import { BaseDTO } from "./BaseDTO";

/** ObjectDTO */
export class ObjectDTO extends BaseDTO {

	/** 1a81bc99-28c2-4c03-ac5c-a1de4967cc36 is the Id of ObjectDTO type. */
	static get TypeId(): Guid { return Guid.parse("1a81bc99-28c2-4c03-ac5c-a1de4967cc36"); }

	/** Properties */
	Properties?: PropertyDTO[];
};