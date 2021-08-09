import { CommandDTO } from "src/api/CommandDTO";
import { PropertyDTO } from "src/api/PropertyDTO";

export class CommandWrapper {
   private _dto: CommandDTO;
   constructor(dto: CommandDTO){
      this._dto = dto;
   }
   public get DTO(): CommandDTO { return this._dto; }
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
}
