import { Guid} from "guid-typescript";
import { AdressDTO } from "./AdressDTO";
import { FindAdressesAccess } from "../api/FindAdressesAccess";
import { CommandDTO } from "../api/CommandDTO";

/**
 */
export class FindAdresses extends FindAdressesAccess {

	constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : FindAdressesAccess.TypeId)}

};