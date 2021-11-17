import { Guid } from "guid-typescript";
import { equalsGuid } from "src/utils";
import { ObjectTypeDTO } from "./ObjectTypeDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 */
export class GetObjectTypesBase  extends CommandWrapper {

   constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : GetObjectTypesBase.TypeId)}

   /** 058aa634-956c-4167-b5e0-2c2171ae55c0 is the Id of GetObjectTypes type. */
   static get TypeId(): Guid { return Guid.parse("058aa634-956c-4167-b5e0-2c2171ae55c0"); }

   /** Checks if the type of the DTO fits */
   static IsForMe(dto: CommandDTO) { return equalsGuid(dto.Type, GetObjectTypesBase.TypeId); }

      /**  */
   get Result() : ObjectTypeDTO[]|undefined {
      let result = this.getArgument("Result");
      if (!result)
         return undefined;
      return JSON.parse(result) as ObjectTypeDTO[] ;
   }

   /// <summary>Calls the command</summary>
   execute(): Promise<ObjectTypeDTO[]|undefined> {
      console.log('call ' + JSON.stringify(this.DTO));
      return this.Service.executeCommand(this.DTO)
      .then((cmd) => {
         console.log('return with result ' + JSON.stringify(cmd));
         return new GetObjectTypesBase(cmd).Result;
      })
      .catch((e) =>{
         console.log('return with error ' + JSON.stringify(e));
         return new Promise<ObjectTypeDTO[]>(null);
      });
   }
};