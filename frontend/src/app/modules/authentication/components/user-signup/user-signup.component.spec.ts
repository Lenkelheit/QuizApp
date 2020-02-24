import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UserSignupComponent } from './user-signup.component';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { of } from 'rxjs';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('UserSignupComponent', () => {
    let component: UserSignupComponent;
    let fixture: ComponentFixture<UserSignupComponent>;
    const authenticationServiceSpy = jasmine.createSpyObj('AuthenticationService', ['getCurrentUser', 'login']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [UserSignupComponent],
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
        fixture = TestBed.createComponent(UserSignupComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        authenticationServiceSpy.getCurrentUser.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });
});
