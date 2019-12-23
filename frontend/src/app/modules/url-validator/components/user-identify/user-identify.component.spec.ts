import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UserIdentifyComponent } from './user-identify.component';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from 'src/app/shared/shared.module';
import { UrlValidatorService } from 'src/app/services/url-validator.service';
import { UrlService } from 'src/app/services/url.service';
import { of } from 'rxjs';
import { IdentityUrlDto } from 'src/app/models/url-validator/identity-url-dto';

describe('UserIdentifyComponent', () => {
    let component: UserIdentifyComponent;
    let fixture: ComponentFixture<UserIdentifyComponent>;
    const urlValidatorServiceSpy = jasmine.createSpyObj('UrlValidatorService', ['checkIsUrlValid', 'identifyUser']);
    const urlServiceSpy = jasmine.createSpyObj('UrlService', ['getTestByUrlId']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [UserIdentifyComponent],
            imports: [
                RouterTestingModule,
                SharedModule
            ],
            providers: [
                { provide: UrlValidatorService, useValue: urlValidatorServiceSpy },
                { provide: UrlService, useValue: urlServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(UserIdentifyComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        urlValidatorServiceSpy.checkIsUrlValid.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should send url on validation', () => {
        urlValidatorServiceSpy.checkIsUrlValid.and.returnValue(of());
        fixture.detectChanges();
        component.identityUrl = {} as IdentityUrlDto;
        urlValidatorServiceSpy.identifyUser.and.returnValue(of());

        component.sendUrlOnValidation();

        expect(urlValidatorServiceSpy.identifyUser).toHaveBeenCalled();
        expect(urlValidatorServiceSpy.identifyUser).toHaveBeenCalledWith(component.identityUrl);
    });
});
