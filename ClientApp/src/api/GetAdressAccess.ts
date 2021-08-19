import { Guid} from "guid-typescript";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 */
export class GetAdressAccess  extends CommandWrapper {

	constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetAdressAccess.TypeId)}

	/** c6771f60-a64b-4775-a006-a2bce00b23a4 is the Id of GetAdress type. */
	static get TypeId(): Guid { return Guid.parse("c6771f60-a64b-4775-a006-a2bce00b23a4"); }

	/** Checks if the type of the DTO fits */
	static IsForMe(dto: CommandDTO) { return Guid.parse(dto.Type) === GetAdressAccess.TypeId; }

	/** Id */
	get Id() : Guid{
		return Guid.parse(this.getArgument("Id"));
	}
	set Id( val : Guid) {
		this.setArgument("Id", val.toString());
	}

	/**  */
	get Adress() : AdressDTO{
		return JSON.parse(this.getArgument("Adress")) as AdressDTO ;
	}
	set Adress( val : AdressDTO) {
		this.setArgument("Adress", JSON.stringify(val));
	}

};