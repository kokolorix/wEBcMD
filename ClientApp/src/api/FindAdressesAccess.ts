import { Guid} from "guid-typescript";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * Addresses search, with multiple tokens
 */
export class FindAdressesAccess  extends CommandWrapper {

	constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : FindAdressesAccess.TypeId)}

	/** 13b2f4da-711a-451e-b435-2c2dc1fbbe4e is the Id of FindAdresses type. */
	static get TypeId(): Guid { return Guid.parse("13b2f4da-711a-451e-b435-2c2dc1fbbe4e"); }

	/** Checks if the type of the DTO fits */
	static IsForMe(dto: CommandDTO) { return Guid.parse(dto.Type) === FindAdressesAccess.TypeId; }

	/** Search text, can contain several words separated by spaces */
	get SearchText() : string{
		return this.getArgument("SearchText");
	}
	set SearchText( val : string) {
		this.setArgument("SearchText", val);
	}

	/** The result of the search is a list of AddressDTO objects */
	get SearchResult() : AdressDTO[]{
		return JSON.parse(this.getArgument("SearchResult")) as AdressDTO[] ;
	}
	set SearchResult( val : AdressDTO[]) {
		this.setArgument("SearchResult", JSON.stringify(val));
	}

};