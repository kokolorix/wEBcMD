import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommandDTO } from 'src/api/CommandDTO';
import { SampleCommandWrapper } from 'src/impl/SampleCommandWrapper';

@Component({
   selector: 'app-home',
   templateUrl: './home.component.html',
   styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
   public forecasts: WeatherForecast[];
   private _http : HttpClient;

   constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      this._http = http;
      http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
         this.forecasts = result;
      }, error => console.error(error));
   }

   ngOnInit(): void {
   }

   executeCommand(): void {
      let cmd = new CommandDTO();
      let sample = new SampleCommandWrapper(cmd);
      sample.FirstOne = "The First";
      // sample.Ar[]
      // this._http.post(baseUrl + 'weatherforecast').subscribe(result => {
      // }
   }

}

interface WeatherForecast {
   date: string;
   temperatureC: number;
   temperatureF: number;
   summary: string;
}
