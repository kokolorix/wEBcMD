import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/** SampleCommand2 */
export class SampleCommand2Access  extends CommandWrapper {

	constructor(dto: CommandDTO){super(dto)}

		/** cb0ab4ab-9aeb-4b42-ac2d-152a4555370a is the Id of SampleCommand2 type. */
		static get TypeId() :string { return "cb0ab4ab-9aeb-4b42-ac2d-152a4555370a"; }

		/** Checks if the type of the DTO fits */
		static IsForMe(dto: CommandDTO) { return dto.Type === SampleCommand2Access.TypeId; }

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