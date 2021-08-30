import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Guid } from 'guid-typescript';
import { SetAdress } from 'src/impl/SetAdress';
import { AdressDTO } from 'src/api/AdressDTO';

@Component({
   selector: 'app-home',
   templateUrl: './home.component.html',
   styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
   public forecasts: WeatherForecast[];
   private _http : HttpClient;
   private _baseUrl: string;

   constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      this._http = http;
      this._baseUrl = baseUrl;
      http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
         this.forecasts = result;
      }, error => console.error(error));
   }

   ngOnInit(): void {
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
      setAdress.execute(Guid.createEmpty(), adress).then(
         a => this._adress = a).then(a=>this._adress = a
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

interface WeatherForecast {
   date: string;
   temperatureC: number;
   temperatureF: number;
   summary: string;
}
