import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TestResultsComponent } from './test-results.component';
import { Observable, of } from 'rxjs';
import { TestService } from 'src/app/services/test.service';
import { SharedModule } from 'src/app/shared/shared.module';

describe('TestResultsComponent', () => {
    let component: TestResultsComponent;
    let fixture: ComponentFixture<TestResultsComponent>;
    const testServiceSpy = jasmine.createSpyObj('TestService', ['getResultsByTestId']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [TestResultsComponent],
            imports: [
                SharedModule
            ],
            providers: [
                { provide: TestService, useValue: testServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TestResultsComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.getTestId$ = new Observable();

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should set results page', () => {
        component.getTestId$ = new Observable();
        fixture.detectChanges();
        const testId = 1, pageIndex = 1, pageSize = 10;
        testServiceSpy.getResultsByTestId.and.returnValue(of());

        component.setResultsPage(testId, pageIndex, pageSize);

        expect(testServiceSpy.getResultsByTestId).toHaveBeenCalled();
        expect(testServiceSpy.getResultsByTestId).toHaveBeenCalledWith(testId, pageIndex, pageSize);
    });
});
