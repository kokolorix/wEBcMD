import { PropertyDTO } from "./PropertyDTO";
import { BaseDTO } from "./BaseDTO";

/**
 *          Base command object. Has all arguments in generig list,          can be wrapped by concrete command wrappers         
 */
export class CommandDTO extends BaseDTO {
	/** Indicates if this is the answer */
	Response?: boolean;
	/** Arguments of the command */
	Arguments?: PropertyDTO[];
};