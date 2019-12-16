import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserLoginDto } from 'src/app/models/authentication/user-login-dto';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Error } from 'src/app/models/error/error';
import { UserLoggedinDto } from 'src/app/models/authentication/user-loggedin-dto';

@Component({
    selector: 'app-user-login',
    templateUrl: './user-login.component.html',
    styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent {
    public userLogin: UserLoginDto = {} as UserLoginDto;
    public userLoggedin: UserLoggedinDto;

    public errors: Error;

    constructor(private authenticationService: AuthenticationService, private router: Router) { }

    public loginUser() {
        this.authenticationService.login(this.userLogin).subscribe(userLoggedinResp => {
            this.userLoggedin = userLoggedinResp.body;
            this.router.navigate(['/tests']);
        },
            (respErrors: HttpErrorResponse) => {
                this.errors = respErrors.error;
                this.clearUserPassword();
            }
        );
    }

    private clearUserPassword() {
        this.userLogin.password = null;
    }
}
