import { Guid} from "guid-typescript";
import { DeleteAdressBase } from "../api/DeleteAdressBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 * Delete the Adress with the given id.
 *          Returns the Adress which was deleted.
 */
export class DeleteAdress extends DeleteAdressBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : DeleteAdressBase.TypeId)}

};