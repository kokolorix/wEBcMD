import { Guid} from "guid-typescript";
import { PropertyTypeDTO } from "./PropertyTypeDTO";
import { TypeDTO } from "./TypeDTO";

/** ObjectTypeDTO */
export class ObjectTypeDTO extends TypeDTO {

	/** 38b38794-0a2e-466c-b198-c831708298f6 is the Id of ObjectTypeDTO type. */
	static get TypeId(): Guid { return Guid.parse("38b38794-0a2e-466c-b198-c831708298f6"); }

	/** Category */
	Category?: string;
	/** Name */
	Name?: string;
	/** PropertyTypes */
	PropertyTypes?: PropertyTypeDTO[];
};