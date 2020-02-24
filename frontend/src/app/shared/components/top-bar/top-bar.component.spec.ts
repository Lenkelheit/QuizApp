import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TopBarComponent } from './top-bar.component';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from '../../shared.module';
import { of } from 'rxjs';

describe('TopBarComponent', () => {
    let component: TopBarComponent;
    let fixture: ComponentFixture<TopBarComponent>;
    const authenticationServiceSpy = jasmine.createSpyObj('AuthenticationService', ['logout']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                SharedModule,
                RouterTestingModule
            ],
            providers: [
                { provide: AuthenticationService, useValue: authenticationServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TopBarComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should check that "logout" is called', () => {
        authenticationServiceSpy.logout.and.returnValue(of());

        component.logout();

        expect(authenticationServiceSpy.logout).toHaveBeenCalled();
    });
});
