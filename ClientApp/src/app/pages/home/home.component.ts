import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommandDTO } from 'src/api/CommandDTO';
import { SampleCommand } from 'src/impl/SampleCommand';
import { error } from '@angular/compiler/src/util';

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
      let sample = new SampleCommand();
      sample.FirstOne = "The First";
      // sample.Ar[]
      const headers = new HttpHeaders().set("Content-Type", 'application/json');
      this._http.post<CommandDTO>(
         this._baseUrl + 'Command/execute',
         sample.DTO
         , { headers, responseType: "json" }
      ).subscribe(result => {
         console.log(result);
      }, error => {
            console.error(error)
         });
   }

}

interface WeatherForecast {
   date: string;
   temperatureC: number;
   temperatureF: number;
   summary: string;
}
