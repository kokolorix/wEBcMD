import { SampleCommandAccess } from "../api/SampleCommandAccess";
import { CommandDTO } from "../api/CommandDTO";

/**
 * This is the sample caommand. He has two Parameters
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

	constructor(dto: CommandDTO) { super(dto) }

};