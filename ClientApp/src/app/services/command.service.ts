import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { CommandDTO } from 'src/api/CommandDTO';

@Injectable({
  providedIn: 'root'
})
export class CommandService {
   protected _http: HttpClient;
   protected _baseUri: string;

   private static _service : CommandService;
   public static get Service() : CommandService { return CommandService._service; }

   constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      this._http = http;
      this._baseUri = baseUrl;
      CommandService._service = this;
   }

   executeCommand(command: CommandDTO): Promise<CommandDTO> {
      const headers = new HttpHeaders().set("Content-Type", 'application/json');
      return this._http.post<CommandDTO>(
         this._baseUri + 'Command/execute', command, { headers, responseType: "json" }
      ).toPromise<CommandDTO>();
   }

}
