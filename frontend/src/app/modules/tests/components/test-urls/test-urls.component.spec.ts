import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TestUrlsComponent } from './test-urls.component';
import { Observable, of } from 'rxjs';
import { TestService } from 'src/app/services/test.service';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from 'src/app/shared/shared.module';

describe('TestUrlsComponent', () => {
    let component: TestUrlsComponent;
    let fixture: ComponentFixture<TestUrlsComponent>;
    const testServiceSpy = jasmine.createSpyObj('TestService', ['getUrlsByTestId']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [TestUrlsComponent],
            imports: [
                RouterTestingModule,
                SharedModule
            ],
            providers: [
                { provide: TestService, useValue: testServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TestUrlsComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.getTestId$ = new Observable();

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should set urls page', () => {
        component.getTestId$ = new Observable();
        fixture.detectChanges();
        const testId = 1, pageIndex = 1, pageSize = 10;
        testServiceSpy.getUrlsByTestId.and.returnValue(of());

        component.setUrlsPage(testId, pageIndex, pageSize);

        expect(testServiceSpy.getUrlsByTestId).toHaveBeenCalled();
        expect(testServiceSpy.getUrlsByTestId).toHaveBeenCalledWith(testId, pageIndex, pageSize);
    });
});
