import { Guid} from "guid-typescript";
import { ArgumentDTO } from "../api/ArgumentDTO";
import { EchoArgumentsBase } from "../api/EchoArgumentsBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 * A  command that returns the given arguments
 */
export class EchoArguments extends EchoArgumentsBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : EchoArgumentsBase.TypeId)}

};