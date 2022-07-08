import { Guid} from "guid-typescript";
import { ValueDTO } from "../api/ValueDTO";
import { EchoValueBase } from "../api/EchoValueBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 * A command, that returns the given value
 */
export class EchoValue extends EchoValueBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : EchoValueBase.TypeId)}

};