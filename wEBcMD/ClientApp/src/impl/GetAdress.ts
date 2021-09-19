import { Guid} from "guid-typescript";
import { GetAdressBase } from "../api/GetAdressBase";
import { CommandDTO } from "../api/CommandDTO";

/**
 */
export class GetAdress extends GetAdressBase {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetAdressBase.TypeId)}

};