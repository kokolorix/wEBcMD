import { Guid} from "guid-typescript";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 */
export class FindAdressesAccess  extends CommandWrapper {

	constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : FindAdressesAccess.TypeId)}

	/** 13b2f4da-711a-451e-b435-2c2dc1fbbe4e is the Id of FindAdresses type. */
	static get TypeId(): Guid { return Guid.parse("13b2f4da-711a-451e-b435-2c2dc1fbbe4e"); }

	/** Checks if the type of the DTO fits */
	static IsForMe(dto: CommandDTO) { return Guid.parse(dto.Type) === FindAdressesAccess.TypeId; }

	/** SearchText */
	get SearchText() : string{
		return this.getArgument("SearchText");
	}
	set SearchText( val : string) {
		this.setArgument("SearchText", val);
	}

	/**  */
	get SearchResult() : AdressDTO[]{
		return this.getArgument("SearchResult");
	}
	set SearchResult( val : AdressDTO[]) {
		this.setArgument("SearchResult", val);
	}

};