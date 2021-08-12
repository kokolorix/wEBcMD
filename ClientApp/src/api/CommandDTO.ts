import { Guid} from "guid-typescript";
import { PropertyDTO } from "./PropertyDTO";
import { BaseDTO } from "./BaseDTO";

/**
 *          Base command object. Has all arguments in generig list,          can be wrapped by concrete command wrappers
 */
export class CommandDTO extends BaseDTO {

	/** c1eda1fc-cc45-4658-889f-ccd989c2848a is the Id of CommandDTO type. */
	static get TypeId(): Guid { return Guid.parse("c1eda1fc-cc45-4658-889f-ccd989c2848a"); }

	/** Indicates if this is the answer */
	Response?: boolean;
	/** Arguments of the command */
	Arguments?: PropertyDTO[] = [];
};
