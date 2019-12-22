import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TestResultListComponent } from './test-result-list.component';
import { TestResultService } from 'src/app/services/test-result.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { of } from 'rxjs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('TestResultListComponent', () => {
    let component: TestResultListComponent;
    let fixture: ComponentFixture<TestResultListComponent>;
    const testResultServiceSpy = jasmine.createSpyObj('TestResultService', ['getTestResults']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [TestResultListComponent],
            imports: [
                BrowserAnimationsModule,
                SharedModule
            ],
            providers: [
                { provide: TestResultService, useValue: testResultServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TestResultListComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        testResultServiceSpy.getTestResults.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should set test results page with filter', () => {
        testResultServiceSpy.getTestResults.and.returnValue(of());
        fixture.detectChanges();
        component.intervieweeNameFilter = 'filter';
        const pageIndex = 1, pageSize = 10;

        component.setTestResultsPageWithFilter(pageIndex, pageSize);

        expect(testResultServiceSpy.getTestResults).toHaveBeenCalled();
        expect(testResultServiceSpy.getTestResults).toHaveBeenCalledWith(component.intervieweeNameFilter, pageIndex, pageSize)
    });
});
