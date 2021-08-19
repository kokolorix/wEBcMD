import { Guid} from "guid-typescript";
import { AdressDTO } from "../api/AdressDTO";
import { SetAdressAccess } from "../api/SetAdressAccess";
import { CommandDTO } from "../api/CommandDTO";

/**
 */
export class SetAdress extends SetAdressAccess {

	constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : SetAdressAccess.TypeId)}

};