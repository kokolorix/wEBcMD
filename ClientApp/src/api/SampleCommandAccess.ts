import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

export class SampleCommandAccess  extends CommandWrapper {
	get FirstOne() : string{
		return this.getArgument($quot;FirstOne£quot;);
	}
	set FirstOne( val : string) {
		;
		return this.setArgument($quot;FirstOne£quot;, val);
	}
	get SecondOne() : boolean{
		return Boolean(JSON.parse(this.getArgument($quot;SecondOne£quot;)));
	}
	set SecondOne( val : boolean) {
		;
		return Boolean(JSON.parse(this.getArgument($quot;SecondOne£quot;, $val.toString())));
	}
	constructor(dto: CommandDTO){super(dto)}}};