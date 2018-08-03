import { InjectionToken } from '@angular/core';
import { AppConfig } from './model/app.config.model';

const APP_CONFIG = new InjectionToken<AppConfig>('app.config');
const useValue = {
    apiEndpoint: 'http://localhost:44397/api/product',
    baseUrl: ''//'https://localhost:44397'
};
function getConfig() {
    return Object.assign({}, useValue, { baseUrl: document.getElementsByTagName('base')[0].href });
}

export { AppConfig, APP_CONFIG, useValue, getConfig };
