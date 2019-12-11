import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { UserLoginDto } from '../models/authentication/user-login-dto';
import { UserAuthenticationResultDto } from '../models/authentication/user-authentication-result-dto';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    private routePrefix = '/api/authentication';

    constructor(private httpService: HttpInternalService) { }

    public login(userLoginDto: UserLoginDto) {
        return this.httpService.postRequest<UserAuthenticationResultDto>(`${this.routePrefix}`, userLoginDto);
    }
}
