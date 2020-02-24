import { Component, OnInit } from '@angular/core';
import { UserRegisterDto } from 'src/app/models/authentication/user-register-dto';
import { UserService } from 'src/app/services/user.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Error } from 'src/app/models/error/error';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-user-signup',
    templateUrl: './user-signup.component.html',
    styleUrls: ['./user-signup.component.css']
})
export class UserSignupComponent implements OnInit {
    public userRegister: UserRegisterDto = {} as UserRegisterDto;
    public errors: Error;

    public signupForm: FormGroup;

    constructor(private userService: UserService, private authenticationService: AuthenticationService,
        // tslint:disable-next-line: align
        private formBuilder: FormBuilder, private router: Router, private titleService: Title) { }

    ngOnInit() {
        this.titleService.setTitle('Sign up - QuizTest');

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

        this.signupForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]],
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(64)]]
        });
    }

    get username() {
        return this.signupForm.get('username');
    }

    get email() {
        return this.signupForm.get('email');
    }

    get password() {
        return this.signupForm.get('password');
    }

    public signupUser() {
        this.authenticationService.register(this.userRegister).subscribe(userRegisteredResp => {
            this.userService.currentUser = userRegisteredResp.body;
            this.router.navigate(['/tests']);
        },
            (respErrors: HttpErrorResponse) => {
                this.errors = respErrors.error;
                this.clearUserEmail();
            }
        );
    }

    private clearUserEmail() {
        this.userRegister.email = null;
    }
}
