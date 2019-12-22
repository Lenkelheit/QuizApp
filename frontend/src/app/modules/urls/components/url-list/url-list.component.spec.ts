import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UrlListComponent } from './url-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { UrlService } from 'src/app/services/url.service';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';

describe('UrlListComponent', () => {
    let component: UrlListComponent;
    let fixture: ComponentFixture<UrlListComponent>;
    const urlServiceSpy = jasmine.createSpyObj('UrlService', ['getUrls']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [UrlListComponent],
            imports: [
                RouterTestingModule,
                SharedModule
            ],
            providers: [
                { provide: UrlService, useValue: urlServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(UrlListComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        urlServiceSpy.getUrls.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should set urls page', () => {
        urlServiceSpy.getUrls.and.returnValue(of());
        fixture.detectChanges();
        const pageIndex = 1, pageSize = 10;

        component.setUrlsPage(pageIndex, pageSize);

        expect(urlServiceSpy.getUrls).toHaveBeenCalled();
        expect(urlServiceSpy.getUrls).toHaveBeenCalledWith(pageIndex, pageSize);
    });
});
