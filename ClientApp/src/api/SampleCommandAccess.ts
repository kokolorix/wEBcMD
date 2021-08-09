import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

export class SampleCommandAccess  extends CommandWrapper {
   constructor(dto: CommandDTO){super(dto);}
	get FirstOne() : string{
		return this.getArgument("FirstOne");
	}
	set FirstOne( firstOne : string) {
		this.setArgument("FirstOne", firstOne);
	}
	get SecondOne() : boolean{
		return Boolean(JSON.parse(this.getArgument("SecondOne")));
	}
	set SecondOne( secondOne : boolean) {
		this.setArgument("SecondOne", secondOne.toString());
	}
};
