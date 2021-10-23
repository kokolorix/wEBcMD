import { Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Guid } from 'guid-typescript';
import { SetAdress } from 'src/impl/SetAdress';
import { AdressDTO } from 'src/api/AdressDTO';
import { CommandTypeDTO } from 'src/api/CommandTypeDTO';
import { GetCommandTypes } from 'src/impl/GetCommandTypes';
import { MatProgressBar } from '@angular/material/progress-bar';
import { CommandWrapper } from 'src/impl/CommandWrapper';
import { asGuid } from 'src/utils';
import { MatInput } from '@angular/material/input';

@Component({
   selector: 'app-home',
   templateUrl: './home.component.html',
   styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
   public commandTypes: CommandTypeDTO[];
   public sideNavOpen: boolean = false;
   private _http : HttpClient;
   private _baseUrl: string;

   @ViewChild('progress') progress: MatProgressBar;
   @ViewChild('result') result: MatInput

   constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string ) {
      this._http = http;
      this._baseUrl = baseUrl;
      const wrapper = new GetCommandTypes();

      wrapper.execute()
      .then((res)=> {
         this.commandTypes = res
         this.sideNavOpen = true;
      })
      .catch((error)=>
         console.error(error)
      );
      // http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
      //    this.forecasts = result;
      // }, error => console.error(error));
   }

   ngOnInit(): void {
   }

   public executeCommand1(ct: CommandTypeDTO): void {
      this.progress.mode = 'indeterminate';
      console.trace(ct);

      const cw = new CommandWrapper(null, asGuid(ct.Id));

      ct.Parameters.forEach(p => {
      });

      cw.executeCmd()
      .then(cmd=>{
         setTimeout(() => {
            const resElmt = document.getElementById('Result');
            if(resElmt){
               const res = new CommandWrapper(cmd, asGuid(ct.Id));
              resElmt['value'] = res.getArgument('Result');
            }
          });
      })
      .catch((error)=>{
         console.error(error);
      })
      .finally(()=>{
         this.progress.mode = 'determinate'
      })
      ;
   }

   public executeCommand(): void {
      console.log('call executeCommand')
      let setAdress = new SetAdress();
      let adress: AdressDTO = {
         Name1: "Heiri",
         Name2: "Krüppelsack",
         Adress1: "Am Ende",
         Adress2: "",
         Housenumber: "42",
         City: "Nieniken",
         Postcode: "0000"
      }
      setAdress.execute(Guid.createEmpty(), adress)
      .then(
         a => this._adress = a
      )
      .catch(
         e => console.error(e)
      );

      // sample.Ar[]
      // const headers = new HttpHeaders().set("Content-Type", 'application/json');
      // this._http.post<CommandDTO>(
      //    this._baseUrl + 'Command/execute',
      //    sample.DTO
      //    , { headers, responseType: "json" }
      // ).subscribe(result => {
      //    console.log(result);
      // }, error => {
      //       console.error(error)
      //    });
   }

   _adress: AdressDTO;

}

