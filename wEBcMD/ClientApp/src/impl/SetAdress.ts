import { Guid} from "guid-typescript";
import { AdressDTO } from "../api/AdressDTO";
import { SetAdressBase } from "../api/SetAdressBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 * Updates an existing address if Id is specified,
 * or creates a new one if Id is null.
 * The updated or newly created address is returned in Result.
 */
export class SetAdress extends SetAdressBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : SetAdressBase.TypeId)}

};