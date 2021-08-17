import { Guid} from "guid-typescript";
import { SampleCommandAccess } from "../api/SampleCommandAccess";
import { CommandDTO } from "../api/CommandDTO";

/**
 * This is the sample command. He has two Parameters
 * and a multiline summary.
 * ``` typescript
 * CommandDTO cmd;
 * if(SampleCommand.IsForMe(dto)){
 *    let sample = new SampleCommand(cmd);
 *    console.log(sample.FirstOne);
 * }
 * ```
 */
export class SampleCommand extends SampleCommandAccess {

	constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : SampleCommandAccess.TypeId)}

};