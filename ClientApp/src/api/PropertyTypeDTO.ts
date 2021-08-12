import { Guid} from "guid-typescript";
import { TypeDTO } from "./TypeDTO";

/** PropertyTypeDTO */
export class PropertyTypeDTO extends TypeDTO {

	/** b070edd3-f270-4093-a500-94047d18c7f9 is the Id of PropertyTypeDTO type. */
	static get TypeId(): Guid { return Guid.parse("b070edd3-f270-4093-a500-94047d18c7f9"); }

	/** Name */
	Name?: string;
	/** DataType */
	DataType?: string;
	/** Default */
	Default?: string;
};