import { CommandWrapper } from "./CommandWrapper";
import { SampleCommandWrapperImpl } from "../impl/SampleCommandWrapperImpl";

export class SampleCommandWrapper extends SampleCommandWrapperImpl {
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