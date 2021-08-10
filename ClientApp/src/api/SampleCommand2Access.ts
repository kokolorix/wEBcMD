import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/** SampleCommand2 */
export class SampleCommand2Access  extends CommandWrapper {

	constructor(dto: CommandDTO){super(dto)}

	/** FirstOne */
	get FirstOne() : string{
		return this.getArgument("FirstOne");
	}
	set FirstOne( val : string) {
		this.setArgument("FirstOne", val);
	}

	/** SecondOne */
	get SecondOne() : boolean{
		return Boolean(JSON.parse(this.getArgument("SecondOne")));
	}
	set SecondOne( val : boolean) {
		this.setArgument("SecondOne", val.toString());
	}

};