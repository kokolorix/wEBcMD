import { CommandWrapper } from "../impl/CommandWrapper";

export class SampleCommand2Access  extends CommandWrapper {
	get FirstOne() : string{
		return undefined;
	}
	set FirstOne( firstOne : string) {
		;
	}
	get SecondOne() : boolean{
		return undefined;
	}
	set SecondOne( secondOne : boolean) {
		;
	}
};