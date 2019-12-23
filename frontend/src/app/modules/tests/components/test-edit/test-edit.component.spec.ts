import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TestEditComponent } from './test-edit.component';
import { TestService } from 'src/app/services/test.service';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from 'src/app/shared/shared.module';
import { Component, Input } from '@angular/core';
import { of } from 'rxjs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UpdateTestDto } from 'src/app/models/test/update-test-dto';
import { UpdateQuestionDto } from 'src/app/models/question/update-question-dto';

@Component({ selector: 'app-question-create-edit', template: '' })
class QuestionCreateEditStubComponent {
    @Input() updateQuestion: any;
    @Input() getQuestion$: any;
}

@Component({ selector: 'app-test-urls', template: '' })
class TestUrlsStubComponent {
    @Input() getTestId$: any;
}

@Component({ selector: 'app-test-results', template: '' })
class TestResultsStubComponent {
    @Input() getTestId$: any;
}

describe('TestEditComponent', () => {
    let component: TestEditComponent;
    let fixture: ComponentFixture<TestEditComponent>;
    const testServiceSpy = jasmine.createSpyObj('TestService', ['getTestById', 'updateTest']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                TestEditComponent,
                QuestionCreateEditStubComponent,
                TestUrlsStubComponent,
                TestResultsStubComponent
            ],
            imports: [
                BrowserAnimationsModule,
                RouterTestingModule,
                SharedModule
            ],
            providers: [
                { provide: TestService, useValue: testServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TestEditComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        testServiceSpy.getTestById.and.returnValue(of());

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should check "testForm" is invalid when "updateTest" properties are default', () => {
        testServiceSpy.getTestById.and.returnValue(of());
        component.updateTest = {} as UpdateTestDto;

        fixture.detectChanges();

        expect(component.testForm.valid).toBeFalse();
    });

    it('should check "testForm" is invalid when "updateTest" properties have bad length', () => {
        testServiceSpy.getTestById.and.returnValue(of());
        component.updateTest = {
            title: 't',
            description: 'd',
            timeLimitSeconds: '00:00:00'
        } as UpdateTestDto;

        fixture.detectChanges();

        expect(component.testForm.valid).toBeFalse();
    });

    it('should check "testForm" is invalid when "updateTest" "timeLimitSeconds" is in bad format', () => {
        testServiceSpy.getTestById.and.returnValue(of());
        component.updateTest = {
            title: 'title',
            description: 'description',
            timeLimitSeconds: '100:100:100'
        } as UpdateTestDto;

        fixture.detectChanges();

        expect(component.testForm.valid).toBeFalse();
    });

    it('should check "testForm" is valid when "updateTest" properties have good length and are in good format', () => {
        testServiceSpy.getTestById.and.returnValue(of());
        component.updateTest = {
            title: 'title',
            description: 'description',
            timeLimitSeconds: '20:30:45'
        } as UpdateTestDto;

        fixture.detectChanges();

        expect(component.testForm.valid).toBeTrue();
    });

    it('should send update test', () => {
        testServiceSpy.getTestById.and.returnValue(of());
        fixture.detectChanges();
        component.updateTest = {} as UpdateTestDto;
        testServiceSpy.updateTest.and.returnValue(of());

        component.sendUpdateTest();

        expect(testServiceSpy.updateTest).toHaveBeenCalled();
        expect(testServiceSpy.updateTest).toHaveBeenCalledWith(component.updateTest);
    });

    it('should add new question', () => {
        testServiceSpy.getTestById.and.returnValue(of());
        fixture.detectChanges();
        component.updateTest = {
            testQuestions: [] as UpdateQuestionDto[]
        } as UpdateTestDto;
        const expectedQuestionsCount = 1;

        component.addQuestion();

        expect(component.updateTest.testQuestions.length).toBe(expectedQuestionsCount);
    });

    it('should delete question by index', () => {
        testServiceSpy.getTestById.and.returnValue(of());
        fixture.detectChanges();
        const question = {} as UpdateQuestionDto;
        component.updateTest = {
            testQuestions: [
                question, question, question
            ] as UpdateQuestionDto[]
        } as UpdateTestDto;
        const questionIndex = 1;
        const expectedQuestionsCount = component.updateTest.testQuestions.length - 1;

        component.deleteQuestion(questionIndex);

        expect(component.updateTest.testQuestions.length).toBe(expectedQuestionsCount);
    });

    it('should clear test', () => {
        testServiceSpy.getTestById.and.returnValue(of());
        fixture.detectChanges();
        component.updateTest = {
            id: 1,
            title: 'title',
            description: 'description',
            authorId: 1
        } as UpdateTestDto;
        const expectedTest = {
            id: component.updateTest.id,
            authorId: component.updateTest.authorId,
            lastModifiedDate: component.updateTest.lastModifiedDate,
            testQuestions: []
        } as UpdateTestDto;

        component.clearTest();

        expect(component.updateTest).toEqual(expectedTest);
    });

    it('should set question by index', () => {
        testServiceSpy.getTestById.and.returnValue(of());
        fixture.detectChanges();
        component.updateTest = {
            testQuestions: [] as UpdateQuestionDto[]
        } as UpdateTestDto;
        const questionIndex = 1;
        const question = {} as UpdateQuestionDto;

        component.setQuestion(questionIndex, question);

        expect(component.updateTest.testQuestions[questionIndex]).toBe(question);
    });
});
