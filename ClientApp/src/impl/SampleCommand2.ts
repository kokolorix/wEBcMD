import { Guid} from "guid-typescript";
import { SampleCommand2Access } from "../api/SampleCommand2Access";
import { CommandDTO } from "../api/CommandDTO";

/** SampleCommand2 */
export class SampleCommand2 extends SampleCommand2Access {

	constructor(dto: CommandDTO) { super(dto) }

};