import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router, NavigationEnd } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
    selector: 'app-top-bar',
    templateUrl: './top-bar.component.html',
    styleUrls: ['./top-bar.component.css']
})
export class TopBarComponent implements OnInit {
    public isUserTopBar = false;

    constructor(private userService: UserService, private authenticationService: AuthenticationService, private router: Router) { }

    ngOnInit() {
        this.router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                this.isUserTopBar = event.url.includes('passing-test') || event.url.includes('login');
            }
        });
    }

    public logout() {
        this.authenticationService.logout().subscribe(() => {
            this.userService.currentUser = null;
            this.router.navigate(['/login']);
        });
    }
}
