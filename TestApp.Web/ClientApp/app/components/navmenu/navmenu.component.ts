import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../../service/auth.service';
import { AuthStateModel } from '../../model/auth.state.model';

@Component({
    selector: 'app-navmenu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
    authState$: Observable<AuthStateModel>;

    constructor(
        private authService: AuthService,
    ) { }

    refreshToken() {
        this.authService.refreshTokens()
            .subscribe();
    }

    ngOnInit() {
        this.authState$ = this.authService.state$;

        // This starts up the token refresh preocess for the app
        this.authService.init()
            .subscribe(
            () => { console.log('Startup success'); },
            error => console.log(error)
            );
    }
}
