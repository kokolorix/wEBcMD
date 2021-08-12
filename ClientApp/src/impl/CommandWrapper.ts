import { Guid } from "guid-typescript";
import { CommandDTO } from "src/api/CommandDTO";
import { PropertyDTO } from "src/api/PropertyDTO";

type CommandWrapperFactory = (dto: CommandDTO) => CommandWrapper;
export class CommandWrapper {
   private _dto: CommandDTO;
   constructor(dto: CommandDTO){
      this._dto = dto ? dto : new CommandDTO();
      if(!this._dto.Type)
         this._dto.Type = CommandDTO.TypeId;
      if(!this._dto.Id)
         this._dto.Id = Guid.create();
   }
   public get DTO(): CommandDTO { return this._dto; }

   	/** Indicates if this is the answer */
   get Response(): boolean { return this.DTO?.Response };
	/** Arguments of the command */
   get Arguments(): PropertyDTO[] { return this.DTO?.Arguments };

   public getArgument(name:string){
      return this._dto.Arguments.find(a => a.Name == name)?.Value;
   }
   public setArgument(name: string, value: string){
      let p = this._dto.Arguments.find(a => a.Name == name);
      if(!p){
         p = new PropertyDTO();
         p.Name = name;
         p.Value = value;
         this._dto.Arguments.push(p);
      }
   }

   private static _factories = new Map<Guid, CommandWrapperFactory>();
   static register(typeId: Guid, factory: CommandWrapperFactory): void {
      CommandWrapper._factories.set(typeId, factory);
   }
}
