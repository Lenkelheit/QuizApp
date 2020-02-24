import { Injectable } from '@angular/core';
import { UserLoggedinDto } from '../models/authentication/user-loggedin-dto';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private user: UserLoggedinDto;

    get currentUser() {
        return this.user;
    }

    set currentUser(user: UserLoggedinDto) {
        this.user = user;
    }
}
