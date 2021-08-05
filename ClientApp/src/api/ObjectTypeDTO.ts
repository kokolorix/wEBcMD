import { PropertyTypeDTO } from "./PropertyTypeDTO";

export interface ObjectTypeDTO {
	Category?: string;
	Name?: string;
	PropertyTypes?: PropertyTypeDTO[];
};