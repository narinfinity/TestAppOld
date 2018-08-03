import { Component, OnInit, Inject, Injectable } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../service/auth.service';


@Component({
    selector: 'app-login',
    templateUrl: './login.template.html'
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    response: any;
    products: any;

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthService
    ) { }

    ngOnInit(): void {
        this.loginForm = this.formBuilder.group({
            username: ['', [Validators.required]],
            password: ['', [Validators.required]],
        });
    }

    onSubmit() {
        this.authService.login(this.loginForm.value)
            .subscribe(() => {
                this.ngOnInit();
                this.response = 'Successfully loggedin';
            },
            error => this.response = error);
    }

    getProducts(): void {
        this.authService.getProducts('')
            .subscribe((res) => {
                
                this.products = res.json();
            },
            error => this.response = error);
    }

}
