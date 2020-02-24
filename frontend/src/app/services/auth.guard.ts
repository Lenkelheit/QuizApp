import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UserService } from './user.service';
import { AuthenticationService } from './authentication.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private userService: UserService, private authenticationService: AuthenticationService, private router: Router) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        if (!this.userService.currentUser) {
            return this.authenticationService.getCurrentUser().pipe(map(userLoggedinResp => {
                this.userService.currentUser = userLoggedinResp.body;
                if (!this.userService.currentUser) {
                    this.router.navigate(['/login']);

                    return false;
                }

                return true;
            }));
        }
        return true;
    }
}
