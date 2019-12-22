import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { QuestionCreateEditComponent } from './question-create-edit.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Component, Input } from '@angular/core';
import { UpdateQuestionDto } from 'src/app/models/question/update-question-dto';
import { UpdateQuestionOptionDto } from 'src/app/models/question-option/update-question-option-dto';
import { Observable } from 'rxjs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({ selector: 'app-question-option-create-edit', template: '' })
class QuestionOptionCreateEditStubComponent {
    @Input() updateQuestionOption: any;
    @Input() getOption$: any;
}

describe('QuestionCreateEditComponent', () => {
    let component: QuestionCreateEditComponent;
    let fixture: ComponentFixture<QuestionCreateEditComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                QuestionCreateEditComponent,
                QuestionOptionCreateEditStubComponent
            ],
            imports: [
                BrowserAnimationsModule,
                SharedModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(QuestionCreateEditComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.updateQuestion = {
            testQuestionOptions: [] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should create', () => {
        component.updateQuestion = {
            testQuestionOptions: [] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should check "questionForm" is invalid when "updateQuestion" properties are default', () => {
        component.updateQuestion = {
            testQuestionOptions: [] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();

        fixture.detectChanges();

        expect(component.questionForm.valid).toBeFalse();
    });

    it('should check "questionForm" is invalid when "updateQuestion" properties have bad length', () => {
        component.updateQuestion = {
            text: 't',
            hint: 'h',
            timeLimitSeconds: '00:00:00',
            testQuestionOptions: [] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();

        fixture.detectChanges();

        expect(component.questionForm.valid).toBeFalse();
    });

    it('should check "questionForm" is invalid when "updateQuestion" "timeLimitSeconds" is in bad format', () => {
        component.updateQuestion = {
            text: 'text',
            hint: 'hint',
            timeLimitSeconds: '100:100:100',
            testQuestionOptions: [] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();

        fixture.detectChanges();

        expect(component.questionForm.valid).toBeFalse();
    });

    it('should check "questionForm" is valid when "updateQuestion" properties have good length and are in good format', () => {
        component.updateQuestion = {
            text: 'text',
            hint: 'hint',
            timeLimitSeconds: '20:30:45',
            testQuestionOptions: [] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();

        fixture.detectChanges();

        expect(component.questionForm.valid).toBeTrue();
    });

    it('should add new question option', () => {
        component.updateQuestion = {
            testQuestionOptions: [] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();
        fixture.detectChanges();
        const expectedQuestionOptionsCount = 1;

        component.addQuestionOption();

        expect(component.updateQuestion.testQuestionOptions.length).toBe(expectedQuestionOptionsCount);
    });

    it('should delete question option by index when array with question options has elements more than 1', () => {
        const questionOption = {} as UpdateQuestionOptionDto;
        component.updateQuestion = {
            testQuestionOptions: [
                questionOption, questionOption, questionOption
            ] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();
        fixture.detectChanges();
        const questionOptionIndex = 1;
        const expectedQuestionOptionsCount = component.updateQuestion.testQuestionOptions.length - 1;

        component.deleteQuestionOption(questionOptionIndex);

        expect(component.updateQuestion.testQuestionOptions.length).toBe(expectedQuestionOptionsCount);
    });

    it('should delete question option by index when array with question options has 1 element', () => {
        const questionOption = {} as UpdateQuestionOptionDto;
        component.updateQuestion = {
            testQuestionOptions: [
                questionOption
            ] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();
        fixture.detectChanges();
        const questionOptionIndex = 0;
        const expectedQuestionOptionsCount = 1;

        component.deleteQuestionOption(questionOptionIndex);

        expect(component.updateQuestion.testQuestionOptions.length).toBe(expectedQuestionOptionsCount);
    });

    it('should set question option by index', () => {
        component.updateQuestion = {
            testQuestionOptions: [] as UpdateQuestionOptionDto[]
        } as UpdateQuestionDto;
        component.getQuestion$ = new Observable();
        fixture.detectChanges();
        const questionOptionIndex = 1;
        const questionOption = {} as UpdateQuestionOptionDto;

        component.setQuestionOption(questionOptionIndex, questionOption);

        expect(component.updateQuestion.testQuestionOptions[questionOptionIndex]).toBe(questionOption);
    });
});
