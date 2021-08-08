import { PropertyDTO } from "./PropertyDTO";
import { BaseDTO } from "./BaseDTO";

export class CommandDTO extends BaseDTO {
	Response?: boolean;
	Arguments?: PropertyDTO[];
};