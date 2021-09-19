import { Guid} from "guid-typescript";
import { AdressDTO } from "../api/AdressDTO";
import { FindAdressesBase } from "../api/FindAdressesBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 * Addresses search, with multiple tokens
 */
export class FindAdresses extends FindAdressesBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : FindAdressesBase.TypeId)}

};