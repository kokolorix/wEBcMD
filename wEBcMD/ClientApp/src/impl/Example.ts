import { Guid} from "guid-typescript";
import { ExampleBase } from "../api/ExampleBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 * The very first command we designed
 */
export class Example extends ExampleBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : ExampleBase.TypeId)}

};