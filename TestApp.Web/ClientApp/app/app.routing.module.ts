import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';

import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';

import { AuthService } from './service/auth.service';

import { AppConfig, APP_CONFIG, useValue, getConfig } from '../app/app.config';

const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },

    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },

    { path: 'home', component: HomeComponent },
    { path: 'counter', component: CounterComponent },
    { path: 'fetchdata', component: FetchDataComponent },
    { path: '**', redirectTo: 'login' }
];

import { JL } from 'jsnlog';
JL().setOptions({ "level": JL.getWarnLevel() });
export class UncaughtExceptionHandler implements ErrorHandler {
    handleError(error: any) {
        JL().fatalException('Uncaught Exception', error);
    }
}
@NgModule({
   
    declarations: [
        AppComponent,
        NavMenuComponent,

        LoginComponent,
        RegisterComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent
    ],
    imports: [ 
        CommonModule,
        ReactiveFormsModule, // <-- #2 add to @NgModule imports
        HttpModule,
        FormsModule,
        RouterModule.forRoot(routes)// , { preloadingStrategy: PreloadAllModules }
    ],    
    providers: [
        { provide: AuthService, useClass: AuthService },
        { provide: APP_CONFIG, useValue: useValue },
        { provide: ErrorHandler, useClass: UncaughtExceptionHandler },
        { provide: 'JSNLOG', useValue: JL }
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
