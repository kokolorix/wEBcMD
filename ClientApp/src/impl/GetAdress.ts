import { Guid} from "guid-typescript";
import { AdressDTO } from "../api/AdressDTO";
import { GetAdressAccess } from "../api/GetAdressAccess";
import { CommandDTO } from "../api/CommandDTO";

/**
 */
export class GetAdress extends GetAdressAccess {

	constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetAdressAccess.TypeId)}

};