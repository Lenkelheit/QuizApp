import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UrlResultsComponent } from './url-results.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { UrlService } from 'src/app/services/url.service';
import { Observable, of } from 'rxjs';

describe('UrlResultsComponent', () => {
    let component: UrlResultsComponent;
    let fixture: ComponentFixture<UrlResultsComponent>;
    const urlServiceSpy = jasmine.createSpyObj('UrlService', ['getTestResultsByUrlId']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [UrlResultsComponent],
            imports: [
                SharedModule
            ],
            providers: [
                { provide: UrlService, useValue: urlServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(UrlResultsComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.getUrlId$ = new Observable();

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should set test results page', () => {
        component.getUrlId$ = new Observable();
        fixture.detectChanges();
        const urlId = 1, pageIndex = 1, pageSize = 10;
        urlServiceSpy.getTestResultsByUrlId.and.returnValue(of());

        component.setTestResultsPage(urlId, pageIndex, pageSize);

        expect(urlServiceSpy.getTestResultsByUrlId).toHaveBeenCalled();
        expect(urlServiceSpy.getTestResultsByUrlId).toHaveBeenCalledWith(urlId, pageIndex, pageSize);
    });
});
