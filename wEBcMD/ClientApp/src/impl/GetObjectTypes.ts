import { Guid} from "guid-typescript";
import { GetObjectTypesBase } from "../api/GetObjectTypesBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 */
export class GetObjectTypes extends GetObjectTypesBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetObjectTypesBase.TypeId)}

};