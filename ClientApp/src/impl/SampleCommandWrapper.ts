import { CommandDTO } from "src/api/CommandDTO";
import { SampleCommandAccess } from "../api/SampleCommandAccess";

export class SampleCommandWrapper extends SampleCommandAccess {
   constructor(dto: CommandDTO){super(dto);}
};
