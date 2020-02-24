import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserLoginDto } from 'src/app/models/authentication/user-login-dto';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Error } from 'src/app/models/error/error';
import { UserService } from 'src/app/services/user.service';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-user-login',
    templateUrl: './user-login.component.html',
    styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {
    public userLogin: UserLoginDto = {} as UserLoginDto;

    public errors: Error;

    constructor(private userService: UserService, private authenticationService: AuthenticationService,
        // tslint:disable-next-line: align
        private router: Router, private titleService: Title) { }

    ngOnInit() {
        this.titleService.setTitle('Log in - QuizTest');

        if (this.userService.currentUser) {
            this.router.navigate(['/tests']);
        } else {
            this.authenticationService.getCurrentUser().subscribe(userLoggedinResp => {
                this.userService.currentUser = userLoggedinResp.body;
                if (this.userService.currentUser) {
                    this.router.navigate(['/tests']);
                }
            });
        }
    }

    public loginUser() {
        this.authenticationService.login(this.userLogin).subscribe(userLoggedinResp => {
            this.userService.currentUser = userLoggedinResp.body;
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
