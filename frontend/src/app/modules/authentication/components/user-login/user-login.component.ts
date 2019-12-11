import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserLoginDto } from 'src/app/models/authentication/user-login-dto';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { UserAuthenticationResultDto } from 'src/app/models/authentication/user-authentication-result-dto';

@Component({
    selector: 'app-user-login',
    templateUrl: './user-login.component.html',
    styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {
    public userLogin: UserLoginDto = {} as UserLoginDto;
    public userAuthenticationResult: UserAuthenticationResultDto;

    public errors: Error;

    constructor(private authenticationService: AuthenticationService, private formBuilder: FormBuilder, private router: Router) { }

    ngOnInit() {
    }

    public loginUser() {
        this.authenticationService.login(this.userLogin).subscribe(userAuthenticationResultResp => {

            this.userAuthenticationResult = userAuthenticationResultResp.body;
            console.log(userAuthenticationResultResp);
        });
    }
}
