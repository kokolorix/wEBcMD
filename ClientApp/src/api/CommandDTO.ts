import { PropertyDTO } from "./PropertyDTO";

export interface CommandDTO {
	Response?: boolean;
	Arguments?: PropertyDTO[];
};