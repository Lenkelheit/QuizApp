import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { UserLoginDto } from '../models/authentication/user-login-dto';
import { UserLoggedinDto } from '../models/authentication/user-loggedin-dto';
import { UserRegisterDto } from '../models/authentication/user-register-dto';
import { UserRegisteredDto } from '../models/authentication/user-registered-dto';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    private routePrefix = '/api/authentication';

    constructor(private httpService: HttpInternalService) { }

    public getCurrentUser() {
        return this.httpService.getRequest<UserLoggedinDto>(`${this.routePrefix}`);
    }

    public register(userRegisterDto: UserRegisterDto) {
        return this.httpService.postRequest<UserRegisteredDto>(`${this.routePrefix}/register`, userRegisterDto);
    }

    public login(userLoginDto: UserLoginDto) {
        return this.httpService.postRequest<UserLoggedinDto>(`${this.routePrefix}/login`, userLoginDto);
    }

    public logout() {
        return this.httpService.getRequest<void>(`${this.routePrefix}/logout`);
    }
}
