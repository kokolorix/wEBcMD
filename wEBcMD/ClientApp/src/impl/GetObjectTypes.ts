import { Guid} from "guid-typescript";
import { GetObjectTypesBase } from "../api/GetObjectTypesBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 * Retrieval of all object types known in the system
 */
export class GetObjectTypes extends GetObjectTypesBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetObjectTypesBase.TypeId)}

};