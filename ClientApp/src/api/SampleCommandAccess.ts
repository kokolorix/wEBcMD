import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

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
export class SampleCommandAccess  extends CommandWrapper {

	constructor(dto: CommandDTO){super(dto)}

		/** e3e185bd-5237-4574-977f-a040bbe12d35 is the Id of SampleCommand type. */
		static get TypeId() :string { return "e3e185bd-5237-4574-977f-a040bbe12d35"; }

		/** Checks if the type of the DTO fits */
		static IsForMe(dto: CommandDTO) { return dto.Type === SampleCommandAccess.TypeId; }

	/**
	 * The FirstOne is a string parameter
	 * and has a multiline comment
	 */
	get FirstOne() : string{
		return this.getArgument("FirstOne");
	}
	set FirstOne( val : string) {
		this.setArgument("FirstOne", val);
	}

	/** The SecondOne is a boolean parameter */
	get SecondOne() : boolean{
		return Boolean(JSON.parse(this.getArgument("SecondOne")));
	}
	set SecondOne( val : boolean) {
		this.setArgument("SecondOne", val.toString());
	}

};