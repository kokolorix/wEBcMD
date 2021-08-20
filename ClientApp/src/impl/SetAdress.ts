import { Guid} from "guid-typescript";
import { AdressDTO } from "../api/AdressDTO";
import { SetAdressAccess } from "../api/SetAdressAccess";
import { CommandDTO } from "../api/CommandDTO";

/**
 * Updates an existing address if Id is specified,
 * or creates a new one if Id is null.
 * The updated or newly created address is returned in Result.
 */
export class SetAdress extends SetAdressAccess {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : SetAdressAccess.TypeId)}

};