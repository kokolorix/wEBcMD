import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParameterCodec } from '@angular/common/http';
import { InjectionToken } from '@angular/core';
import { CommandDTO } from './CommandDTO';
import { Observable } from 'rxjs';
import { exception } from 'console';
export const BASE_PATH = new InjectionToken<string>('basePath');

export class CommandService {

   protected basePath = 'https://localhost';
   private _defaultHeaders = new HttpHeaders();
   public get defaultHeaders() {
      return this._defaultHeaders;
   }
   public set defaultHeaders(value) {
      this._defaultHeaders = value;
   }
   // public configuration = new Configuration();
   public encoder: HttpParameterCodec;

   constructor(protected httpClient: HttpClient, @Optional() @Inject(BASE_PATH) basePath: string) {
      //  if (configuration) {
      //      this.configuration = configuration;
      //  }
      //  if (typeof this.configuration.basePath !== 'string') {
      //      if (typeof basePath !== 'string') {
      //          basePath = this.basePath;
      //      }
      //      this.configuration.basePath = basePath;
      //  }
      //  this.encoder = this.configuration.encoder || new CustomHttpParameterCodec();
      // apiV1AdressenGuidGet(guid: string, extraHttpRequestParams?: any): Observable<AdresseDTO>;
   //    public apiV1AdressenGuidGet(guid: string, observe?: 'body', reportProgress?: boolean, options?: { httpHeaderAccept?: 'text/plain' | 'application/json' | 'text/json' }): Observable<CommandDTO> {
   //    throw new Exception { te};
   // };
// }
}

};

