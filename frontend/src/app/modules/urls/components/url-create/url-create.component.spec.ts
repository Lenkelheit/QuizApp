import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UrlCreateComponent } from './url-create.component';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from 'src/app/shared/shared.module';
import { TestService } from 'src/app/services/test.service';
import { UrlService } from 'src/app/services/url.service';
import { of } from 'rxjs';
import { NewUrlDto } from 'src/app/models/url/new-url-dto';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('UrlCreateComponent', () => {
    let component: UrlCreateComponent;
    let fixture: ComponentFixture<UrlCreateComponent>;
    const testServiceSpy = jasmine.createSpyObj('TestService', ['getTests']);
    const urlServiceSpy = jasmine.createSpyObj('UrlService', ['createUrl']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [UrlCreateComponent],
            imports: [
                BrowserAnimationsModule,
                RouterTestingModule,
                SharedModule
            ],
            providers: [
                { provide: TestService, useValue: testServiceSpy },
                { provide: UrlService, useValue: urlServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(UrlCreateComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        testServiceSpy.getTests.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should check "urlForm" is invalid when "newUrl" properties are default', () => {
        testServiceSpy.getTests.and.returnValue(of());
        component.newUrl = {} as NewUrlDto;

        fixture.detectChanges();

        expect(component.urlForm.valid).toBeFalse();
    });

    it('should check "urlForm" is invalid when "newUrl" "intervieweeName" has bad length', () => {
        testServiceSpy.getTests.and.returnValue(of());
        fixture.detectChanges();
        const validFromTime = new Date(), validUntilTime = new Date();
        validFromTime.setHours(validFromTime.getHours() - 1);
        validUntilTime.setHours(validUntilTime.getHours() + 1);
        component.newUrl = {
            testId: 1,
            numberOfRuns: 1,
            validFromTime,
            validUntilTime,
            intervieweeName: 'i'
        } as NewUrlDto;

        fixture.detectChanges();

        expect(component.urlForm.valid).toBeFalse();
    });

    it('should check "urlForm" is invalid when "newUrl" "numberOfRuns" is less than must be', () => {
        testServiceSpy.getTests.and.returnValue(of());
        fixture.detectChanges();
        const validFromTime = new Date(), validUntilTime = new Date();
        validFromTime.setHours(validFromTime.getHours() - 1);
        validUntilTime.setHours(validUntilTime.getHours() + 1);
        component.newUrl = {
            testId: 1,
            numberOfRuns: -1,
            validFromTime,
            validUntilTime,
            intervieweeName: 'intervieweeName'
        } as NewUrlDto;

        fixture.detectChanges();

        expect(component.urlForm.valid).toBeFalse();
    });

    it('should check "urlForm" is invalid when "newUrl" "validUntilTime" is less than "validFromTime"', () => {
        testServiceSpy.getTests.and.returnValue(of());
        const validFromTime = new Date(), validUntilTime = new Date();
        validFromTime.setHours(validFromTime.getHours() + 1);
        validUntilTime.setHours(validUntilTime.getHours() - 1);
        component.newUrl = {
            testId: 1,
            numberOfRuns: 1,
            validFromTime,
            validUntilTime,
            intervieweeName: 'intervieweeName'
        } as NewUrlDto;

        fixture.detectChanges();

        expect(component.urlForm.valid).toBeFalse();
    });

    it('should check "urlForm" is valid when "newUrl" properties are good', () => {
        testServiceSpy.getTests.and.returnValue(of());
        fixture.detectChanges();
        const validFromTime = new Date(), validUntilTime = new Date();
        validFromTime.setHours(validFromTime.getHours() - 1);
        validUntilTime.setHours(validUntilTime.getHours() + 1);
        component.newUrl = {
            testId: 1,
            numberOfRuns: 1,
            validFromTime,
            validUntilTime,
            intervieweeName: 'intervieweeName'
        } as NewUrlDto;

        fixture.detectChanges();

        expect(component.urlForm.valid).toBeTrue();
    });

    it('should send new url', () => {
        testServiceSpy.getTests.and.returnValue(of());
        fixture.detectChanges();
        component.newUrl = {} as NewUrlDto;
        urlServiceSpy.createUrl.and.returnValue(of());

        component.sendNewUrl();

        expect(urlServiceSpy.createUrl).toHaveBeenCalled();
        expect(urlServiceSpy.createUrl).toHaveBeenCalledWith(component.newUrl);
    });

    it('should clear url', () => {
        testServiceSpy.getTests.and.returnValue(of());
        fixture.detectChanges();
        component.newUrl = {
            testId: 1,
            numberOfRuns: 1,
            intervieweeName: 'intervieweeName'
        } as NewUrlDto;
        const expectedUrl = {} as NewUrlDto;

        component.clearUrl();

        expect(component.newUrl).toEqual(expectedUrl);
    });
});
