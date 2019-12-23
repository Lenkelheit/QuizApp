import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TestListComponent } from './test-list.component';
import { TestService } from 'src/app/services/test.service';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from 'src/app/shared/shared.module';

describe('TestListComponent', () => {
    let component: TestListComponent;
    let fixture: ComponentFixture<TestListComponent>;
    const testServiceSpy = jasmine.createSpyObj('TestService', ['getTests', 'deleteTest']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [TestListComponent],
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
        fixture = TestBed.createComponent(TestListComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        testServiceSpy.getTests.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should delete test by id', () => {
        testServiceSpy.getTests.and.returnValue(of());
        fixture.detectChanges();
        const testId = 1;
        testServiceSpy.deleteTest.and.returnValue(of());

        component.deleteTest(testId);

        expect(testServiceSpy.deleteTest).toHaveBeenCalled();
        expect(testServiceSpy.deleteTest).toHaveBeenCalledWith(testId);
    });

    it('should set tests page', () => {
        testServiceSpy.getTests.and.returnValue(of());
        fixture.detectChanges();
        const pageIndex = 1, pageSize = 10;

        component.setTestsPage(pageIndex, pageSize);

        expect(component.currentPageIndex).toBe(pageIndex);
        expect(testServiceSpy.getTests).toHaveBeenCalled();
        expect(testServiceSpy.getTests).toHaveBeenCalledWith(pageIndex, pageSize);
    });
});
