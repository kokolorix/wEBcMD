// import { Injector } from "@angular/core";
import { Guid } from "guid-typescript";
import { CommandDTO } from "src/api/CommandDTO";
import { PropertyDTO } from "src/api/PropertyDTO";
// import { AppInjector } from "src/app/app.module";
import { CommandService } from "src/app/services/command.service";

type CommandWrapperFactory = (dto: CommandDTO) => CommandWrapper;
export class CommandWrapper {
   private _dto: CommandDTO;

   // private static _injector = Injector.create({providers: []});
   // protected static get injector() : Injector {
   //    return CommandWrapper._injector;
   // }
   public static _service: CommandService;

   constructor(dto: CommandDTO, type?: Guid){
      this._dto = dto ? dto : new CommandDTO();
      if(!this._dto.Type)
         this._dto.Type = type ? type.toString() : CommandDTO.TypeId.toString();
      if(!this._dto.Id)
         this._dto.Id = Guid.create().toString();

      // this._service = AppInjector.get<CommandService>(CommandService);
   }
   public get DTO(): CommandDTO { return this._dto; }

   	/** Indicates if this is the answer */
   get Response(): boolean { return this.DTO?.Response };
	/** Arguments of the command */
   get Arguments(): PropertyDTO[] { return this.DTO?.Arguments };
	/** Type Guid of the command */
   get Type(): Guid { return Guid.parse(this.DTO?.Type) };
	/** Id of the command */
   get Id(): Guid { return Guid.parse(this.DTO?.Id) };

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
