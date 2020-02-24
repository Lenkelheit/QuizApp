import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TestResultComponent } from './test-result.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Component, Input } from '@angular/core';
import { TestResultService } from 'src/app/services/test-result.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';

@Component({ selector: 'app-test', template: '' })
class TestStubComponent {
    @Input() test: any;
}

@Component({ selector: 'app-question-answer', template: '' })
class QuestionAnswerStubComponent {
    @Input() answer: any;
}

describe('TestResultComponent', () => {
    let component: TestResultComponent;
    let fixture: ComponentFixture<TestResultComponent>;
    const testResultServiceSpy = jasmine.createSpyObj('TestResultService', ['getTestResultById', 'getAnswersByResultId']);
    const authenticationServiceSpy = jasmine.createSpyObj('AuthenticationService', ['getCurrentUser']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                TestResultComponent,
                TestStubComponent,
                QuestionAnswerStubComponent
            ],
            imports: [
                RouterTestingModule,
                SharedModule
            ],
            providers: [
                { provide: TestResultService, useValue: testResultServiceSpy },
                { provide: AuthenticationService, useValue: authenticationServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TestResultComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        authenticationServiceSpy.getCurrentUser.and.returnValue(of());
        testResultServiceSpy.getTestResultById.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should set result answers page', () => {
        authenticationServiceSpy.getCurrentUser.and.returnValue(of());
        testResultServiceSpy.getTestResultById.and.returnValue(of());
        fixture.detectChanges();
        const testResultId = 1, pageIndex = 1, pageSize = 10;
        testResultServiceSpy.getAnswersByResultId.and.returnValue(of());

        component.setResultAnswersPage(testResultId, pageIndex, pageSize);

        expect(testResultServiceSpy.getAnswersByResultId).toHaveBeenCalled();
        expect(testResultServiceSpy.getAnswersByResultId).toHaveBeenCalledWith(testResultId, pageIndex, pageSize);
    });
});
