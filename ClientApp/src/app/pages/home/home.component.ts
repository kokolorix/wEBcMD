import { Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
public forecasts: WeatherForecast[];

constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
    this.forecasts = result;
  }, error => console.error(error));
}

  ngOnInit(): void {
  }

}

interface WeatherForecast {
date: string;
temperatureC: number;
temperatureF: number;
summary: string;
}
