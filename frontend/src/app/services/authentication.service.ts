import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { UserLoginDto } from '../models/authentication/user-login-dto';
import { UserLoggedinDto } from '../models/authentication/user-loggedin-dto';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    private routePrefix = '/api/authentication';

    constructor(private httpService: HttpInternalService) { }

    public checkUserAuthentication() {
        return this.httpService.getRequest<boolean>(`${this.routePrefix}`);
    }

    public login(userLoginDto: UserLoginDto) {
        return this.httpService.postRequest<UserLoggedinDto>(`${this.routePrefix}/login`, userLoginDto);
    }

    public logout() {
        return this.httpService.getRequest<void>(`${this.routePrefix}/logout`);
    }
}
