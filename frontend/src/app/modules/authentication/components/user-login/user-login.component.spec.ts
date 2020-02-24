import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UserLoginComponent } from './user-login.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { of, throwError } from 'rxjs';
import { UserLoginDto } from 'src/app/models/authentication/user-login-dto';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpResponse } from '@angular/common/http';
import { UserLoggedinDto } from 'src/app/models/authentication/user-loggedin-dto';

describe('UserLoginComponent', () => {
    let component: UserLoginComponent;
    let fixture: ComponentFixture<UserLoginComponent>;
    const authenticationServiceSpy = jasmine.createSpyObj('AuthenticationService', ['getCurrentUser', 'login']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [UserLoginComponent],
            imports: [
                SharedModule,
                RouterTestingModule,
                BrowserAnimationsModule
            ],
            providers: [
                { provide: AuthenticationService, useValue: authenticationServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(UserLoginComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        authenticationServiceSpy.getCurrentUser.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should tell Router to navigate when "loginUser" is called and user is successfully logged in', () => {
        authenticationServiceSpy.getCurrentUser.and.returnValue(of());
        fixture.detectChanges();
        authenticationServiceSpy.login.and.returnValue(of({} as HttpResponse<UserLoggedinDto>));
        const router = TestBed.get(Router);
        spyOn(router, 'navigate');
        const userLogin = {
            email: 'email',
            password: 'password'
        } as UserLoginDto;
        component.userLogin = userLogin;

        component.loginUser();

        expect(authenticationServiceSpy.login).toHaveBeenCalled();
        expect(authenticationServiceSpy.login).toHaveBeenCalledWith(userLogin);
        expect(router.navigate).toHaveBeenCalled();
        expect(router.navigate).toHaveBeenCalledWith(['/tests']);
    });

    it('should clear user password when "loginUser" is called and user is not logged in', () => {
        authenticationServiceSpy.getCurrentUser.and.returnValue(of());
        fixture.detectChanges();
        authenticationServiceSpy.login.and.returnValue(throwError({}));
        const userLogin = {
            email: 'email',
            password: 'password'
        } as UserLoginDto;
        component.userLogin = userLogin;

        component.loginUser();

        expect(authenticationServiceSpy.login).toHaveBeenCalled();
        expect(authenticationServiceSpy.login).toHaveBeenCalledWith(userLogin);
        expect(component.userLogin.password).toBeNull();
        expect(component.userLogin.email).toBe(userLogin.email);
    });
});
