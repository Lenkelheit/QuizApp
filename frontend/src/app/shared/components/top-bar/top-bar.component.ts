import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
    selector: 'app-top-bar',
    templateUrl: './top-bar.component.html',
    styleUrls: ['./top-bar.component.css']
})
export class TopBarComponent {
    constructor(private userService: UserService, private authenticationService: AuthenticationService, private router: Router) { }

    public logout() {
        this.authenticationService.logout().subscribe(() => {
            this.userService.currentUser = null;
            this.router.navigate(['/login']);
        });
    }
}
