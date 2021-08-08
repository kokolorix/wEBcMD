import { PropertyTypeDTO } from "./PropertyTypeDTO";
import { TypeDTO } from "./TypeDTO";

export class ObjectTypeDTO extends TypeDTO {
	Category?: string;
	Name?: string;
	PropertyTypes?: PropertyTypeDTO[];
};