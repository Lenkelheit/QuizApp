import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { QuestionAnswerComponent } from './question-answer.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Component, Input } from '@angular/core';
import { ResultAnswerDetailDto } from 'src/app/models/result-answer/result-answer-detail-dto';
import { ResultAnswerOptionDetailDto } from 'src/app/models/result-answer-option/result-answer-option-detail-dto';
import { ResultQuestionOptionDto } from 'src/app/models/question-option/result-question-option-dto';

@Component({ selector: 'app-question-option', template: '' })
class QuestionOptionStubComponent {
    @Input() questionOption: any;
    @Input() answerOption: any;
}

describe('QuestionAnswerComponent', () => {
    let component: QuestionAnswerComponent;
    let fixture: ComponentFixture<QuestionAnswerComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                QuestionAnswerComponent,
                QuestionOptionStubComponent
            ],
            imports: [
                SharedModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(QuestionAnswerComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.answer = {} as ResultAnswerDetailDto;

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should find answer option', () => {
        component.answer = {
            resultAnswerOptions: [
                { optionId: 2 } as ResultAnswerOptionDetailDto,
                { optionId: 1 } as ResultAnswerOptionDetailDto
            ] as ResultAnswerOptionDetailDto[]
        } as ResultAnswerDetailDto;
        fixture.detectChanges();
        const resultQuestionOption = {
            id: 1
        } as ResultQuestionOptionDto;

        const resultAnswerOption = component.findAnswerOption(resultQuestionOption);

        expect(resultAnswerOption).toBeDefined();
        expect(resultAnswerOption.optionId).toBe(resultQuestionOption.id);
    });

    it('should not find answer option', () => {
        component.answer = {
            resultAnswerOptions: [
                { optionId: 2 } as ResultAnswerOptionDetailDto
            ] as ResultAnswerOptionDetailDto[]
        } as ResultAnswerDetailDto;
        fixture.detectChanges();
        const resultQuestionOption = {
            id: 1
        } as ResultQuestionOptionDto;

        const resultAnswerOption = component.findAnswerOption(resultQuestionOption);

        expect(resultAnswerOption).toBeUndefined();
    });
});
