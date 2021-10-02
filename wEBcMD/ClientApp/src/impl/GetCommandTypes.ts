import { Guid} from "guid-typescript";
import { GetCommandTypesBase } from "../api/GetCommandTypesBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 * Get a list of all Command-Types
 */
export class GetCommandTypes extends GetCommandTypesBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetCommandTypesBase.TypeId)}

};