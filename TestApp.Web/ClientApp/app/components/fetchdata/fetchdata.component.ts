import { Component, Inject, Optional, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { AppConfig, APP_CONFIG } from '../../../app/app.config';
import { AuthService } from '../../service/auth.service';

@Component({
    selector: 'app-fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent implements OnInit {
    public forecasts: WeatherForecast[];
    baseUrl: string;

    constructor(
        private http: Http,
        private authService: AuthService,
        @Inject(APP_CONFIG) config: AppConfig
    ) {
        this.baseUrl = config.baseUrl;
        
    }
    ngOnInit(): void {
        this.getSampleData();
    }
    getSampleData(): void {
        this.authService.getProducts(this.baseUrl + '/api/SampleData/WeatherForecasts')
            .subscribe(result => {
                this.forecasts = result.json() as WeatherForecast[];
            }, error => console.error(error));
    }
}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}
