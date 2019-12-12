import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserLoginDto } from 'src/app/models/authentication/user-login-dto';
import { Router } from '@angular/router';
import { UserAuthenticationResultDto } from 'src/app/models/authentication/user-authentication-result-dto';

@Component({
    selector: 'app-user-login',
    templateUrl: './user-login.component.html',
    styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent {
    public userLogin: UserLoginDto = {} as UserLoginDto;
    public userAuthenticationResult: UserAuthenticationResultDto;

    public errors: Error;

    constructor(private authenticationService: AuthenticationService, private router: Router) { }

    public loginUser() {
        this.authenticationService.login(this.userLogin).subscribe(userAuthenticationResultResp => {
            this.userAuthenticationResult = userAuthenticationResultResp.body;
            this.clearUser();
            if (this.userAuthenticationResult.isValid) {
                this.router.navigate(['/tests']);
            }
        });
    }

    private clearUser() {
        this.userLogin = {} as UserLoginDto;
    }
}
