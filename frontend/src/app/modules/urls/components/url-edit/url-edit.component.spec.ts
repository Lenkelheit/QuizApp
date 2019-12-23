import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UrlEditComponent } from './url-edit.component';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from 'src/app/shared/shared.module';
import { UrlService } from 'src/app/services/url.service';
import { of } from 'rxjs';
import { UpdateUrlDto } from 'src/app/models/url/update-url-dto';
import { TestDto } from 'src/app/models/test/test-dto';
import { Component, Input } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({ selector: 'app-url-results', template: '' })
class UrlResultsStubComponent {
    @Input() getUrlId$: any;
}

describe('UrlEditComponent', () => {
    let component: UrlEditComponent;
    let fixture: ComponentFixture<UrlEditComponent>;
    const urlServiceSpy = jasmine.createSpyObj('UrlService', ['getUrlById', 'updateUrl']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                UrlEditComponent,
                UrlResultsStubComponent
            ],
            imports: [
                BrowserAnimationsModule,
                RouterTestingModule,
                SharedModule
            ],
            providers: [
                { provide: UrlService, useValue: urlServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(UrlEditComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        urlServiceSpy.getUrlById.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should send update url', () => {
        urlServiceSpy.getUrlById.and.returnValue(of());
        fixture.detectChanges();
        component.updateUrl = {} as UpdateUrlDto;
        urlServiceSpy.updateUrl.and.returnValue(of());

        component.sendUpdateUrl();

        expect(urlServiceSpy.updateUrl).toHaveBeenCalled();
        expect(urlServiceSpy.updateUrl).toHaveBeenCalledWith(component.updateUrl);
    });

    it('should clear url', () => {
        urlServiceSpy.getUrlById.and.returnValue(of());
        fixture.detectChanges();
        component.updateUrl = {
            id: 1,
            test: {} as TestDto,
            testId: 1,
            intervieweeName: 'intervieweeName'
        } as UpdateUrlDto;
        const expectedUrl = {
            id: component.updateUrl.id,
            test: component.updateUrl.test,
            testId: component.updateUrl.testId
        } as UpdateUrlDto;

        component.clearUrl();

        expect(component.updateUrl).toEqual(expectedUrl);
    });
});
