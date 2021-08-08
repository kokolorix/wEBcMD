import { CommandWrapper } from "./CommandWrapper";
import { SampleCommand2WrapperImpl } from "../impl/SampleCommand2WrapperImpl";

export class SampleCommand2Wrapper extends SampleCommand2WrapperImpl {
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