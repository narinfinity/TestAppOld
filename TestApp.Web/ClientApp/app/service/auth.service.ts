import { Component, Inject, Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response, URLSearchParams } from '@angular/http';
import { AppConfig, APP_CONFIG } from '../../app/app.config';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/first';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/filter';

import 'rxjs/add/observable/of';
import 'rxjs/add/observable/interval';
import 'rxjs/add/observable/throw';

import { Observable } from 'rxjs/Observable';
import { Subscriber } from 'rxjs/Subscriber';
import { Subscription } from 'rxjs/Subscription';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import { RefreshGrantModel } from '../model/refresh.grant.model';
import { ProfileModel } from '../model/profile.model';
import { AuthStateModel } from '../model/auth.state.model';
import { AuthTokenModel } from '../model/auth.tokens.model';
import { RegisterModel } from '../model/register.model';
import { LoginModel } from '../model/login.model';
import { JwtHelper } from 'angular2-jwt';

@Injectable()
export class AuthService {
    private jwtHelper: JwtHelper = new JwtHelper();
    private initalState: AuthStateModel | any = { profile: {}, tokens: {access_token:'', refresh_token:'', id_token:''}, authReady: false };
    private authReady$ = new BehaviorSubject<boolean>(false);
    private state: BehaviorSubject<AuthStateModel>;
    private refreshSubscription$: Subscription;
    baseUrl: string;
    state$: Observable<AuthStateModel>;
    tokens$: Observable<AuthTokenModel | any>;
    profile$: Observable<ProfileModel | any>;
    loggedIn$: Observable<boolean>;

    constructor(
        private http: Http,
        @Inject(APP_CONFIG) config: AppConfig
    ) {
        
        this.baseUrl = config.baseUrl;        

        this.state = new BehaviorSubject<AuthStateModel>(this.initalState);
        this.state$ = this.state.asObservable();

        this.tokens$ = this.state.filter(state => !!state.authReady).map(state => state.tokens);
        this.profile$ = this.state.filter(state => !!state.authReady).map(state => state.profile);

        this.loggedIn$ = this.tokens$.map(tokens => !!tokens);
    }
    init(): Observable<AuthTokenModel> {
        return this.startupTokenRefresh();//.do(() => this.scheduleRefresh());
    }

    register(data: RegisterModel): Observable<Response> {
        return this.http
            .post(`${this.baseUrl}/account/register`, data)
            .catch(res => Observable.throw(res.json()));
    }

    login(user: LoginModel): Observable<any> {        
        return this.getTokens(user, 'password')
            .catch(res => Observable.throw(res.json()))
            //.do(res => this.scheduleRefresh())
            ;
    }

    logout(): void {
        this.updateState({ profile: undefined, tokens: undefined });
        if (this.refreshSubscription$) {
            this.refreshSubscription$.unsubscribe();
        }
        this.removeToken();
    }

    refreshTokens(): Observable<AuthTokenModel> {
        return this.state.first()
            .map(state => state.tokens)
            .flatMap(tokens => this.getTokens({ refresh_token: tokens.refresh_token }, 'refresh_token')
                .catch(error => Observable.throw('Session Expired'))
            );
    }

    private storeToken(tokens: AuthTokenModel): void {
        const previousTokens = this.retrieveTokens();
        if (previousTokens != null && tokens.refresh_token == null) {
            tokens.refresh_token = previousTokens.refresh_token;
        }

        localStorage.setItem('auth-tokens', JSON.stringify(tokens));
    }

    private retrieveTokens(): AuthTokenModel | any {
        const tokensString = localStorage.getItem('auth-tokens');
        const tokensModel: AuthTokenModel = tokensString == null ? {} : JSON.parse(tokensString);
        return tokensModel;
    }

    private removeToken(): void {
        localStorage.removeItem('auth-tokens');
    }

    private updateState(newState: AuthStateModel): void {
        const previousState = this.state.getValue();
        this.state.next(Object.assign({}, previousState, newState));
    }

    private getTokens(
        data: RefreshGrantModel | LoginModel | any,
        grantType: string): Observable<Response>
    {
        const headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        const options = new RequestOptions({ headers: headers });

        Object.assign(data, { grant_type: grantType, scope: 'openid profile roles' });

        const params = new URLSearchParams();
        Object.keys(data)
            .forEach(key => params.append(key, data[key]));
        params.append('client_id', 's6BhdRkqt3');
        params.append('redirect_uri', 'https://www.getpostman.com/oauth2/callback');
        return this.http.post(`${this.baseUrl}/connect/token`, params.toString(), options)
            .do(res => {
                
                const tokens: AuthTokenModel = res.json();
                const now = new Date();
                tokens.expiration_date = new Date(now.getTime() + tokens.expires_in * 1000).getTime().toString();

                const profile: ProfileModel = tokens ? this.jwtHelper.decodeToken(tokens.id_token) : {};
                
                this.storeToken(tokens);
                this.updateState({ authReady: true, tokens, profile });
            });
    }
    // Get: /api/product
    getProducts(url:string): Observable<any> {
        const tokens = this.retrieveTokens();
        const headers = new Headers({
            'Authorization': `${tokens.token_type} ${tokens.access_token}`,
            'Accept': 'application/json, text/plain, */*',            
        });
        const options = new RequestOptions({ headers: headers });


        return this.http.get(url ? url : `${this.baseUrl}/api/product`, options)
            .do(res => {
                
                const products = res.json();               

                return products;
            });
    }
    private startupTokenRefresh(): Observable<AuthTokenModel> {
        return Observable.of(this.retrieveTokens())
            .flatMap((tokens: AuthTokenModel) => {
                if (!tokens) {
                    this.updateState({ authReady: true });
                    return Observable.throw('No token in Storage');
                }
                const profile: ProfileModel = tokens.id_token ? this.jwtHelper.decodeToken(tokens.id_token) : {};
                this.updateState({ tokens, profile });

                if (+tokens.expiration_date > new Date().getTime()) {
                    this.updateState({ authReady: true });
                }

                return this.refreshTokens();
            })
            .catch(error => {
                this.logout();
                this.updateState({ authReady: true });
                return Observable.throw(error);
            });
    }

    private scheduleRefresh(): void {
        this.refreshSubscription$ = this.tokens$
            .first()
            // refresh every half the total expiration time
            .flatMap(tokens => Observable.interval(tokens.expires_in / 2 * 1000))
            .flatMap(() => this.refreshTokens())
            .subscribe();
    }
}

