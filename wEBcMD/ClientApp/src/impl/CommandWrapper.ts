// import { Injectable, Injector, Optional } from "@angular/core";
import { Guid } from "guid-typescript";
import { CommandDTO } from "src/api/CommandDTO";
import { PropertyDTO } from "src/api/PropertyDTO";
import { CommandService } from "src/app/services/command.service";
import { asGuid } from "src/utils";

type CommandWrapperFactory = (dto: CommandDTO) => CommandWrapper;

// @Injectable({
//    providedIn: 'root'
//  })
export class CommandWrapper {
   private _dto: CommandDTO;

   private _service: CommandService;
   public get Service() { return this._service; }

   constructor(dto?: CommandDTO, type?: Guid){
      this._service = CommandService.Service;
      this._dto = dto ? dto : new CommandDTO();
      if(!this._dto.Type)
         this._dto.Type = type ? type.toString() : CommandDTO.TypeId.toString();
      if(!this._dto.Id)
         this._dto.Id = Guid.create().toString();
   }
   public get DTO(): CommandDTO { return this._dto; }

   	/** Indicates if this is the answer */
   get Response(): boolean { return this.DTO?.Response as boolean };
	/** Arguments of the command */
   get Arguments(): PropertyDTO[] { return this.DTO?.Arguments ? this.DTO?.Arguments : [] };
	/** Type Guid of the command */
   get Type(): Guid { return asGuid(this.DTO?.Type) };
	/** Id of the command */
   get Id(): Guid { return asGuid(this.DTO?.Id) };

   public getArgument(name:string){
      return this._dto.Arguments?.find(a => a.Name == name)?.Value;
   }
   public setArgument(name: string, value: string){
      let p = this._dto.Arguments?.find(a => a.Name == name);
      if(!p){
         p = new PropertyDTO();
         p.Name = name;
         p.Value = JSON.parse(value);
         this._dto.Arguments?.push(p);
      }
   }

   /// <summary>Calls the command</summary>
   public executeCmd(): Promise<CommandDTO> {
      console.log('call ' + JSON.stringify(this.DTO));
      const res = this.Service.executeCommand(this.DTO);
      console.log('return with result ' + JSON.stringify(res));
      return res;
   }

   private static _factories = new Map<Guid, CommandWrapperFactory>();
   static register(typeId: Guid, factory: CommandWrapperFactory): void {
      CommandWrapper._factories.set(typeId, factory);
   }
}
