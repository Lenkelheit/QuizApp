import { Component, Input } from '@angular/core';
import { ResultAnswerDetailDto } from 'src/app/models/result-answer/result-answer-detail-dto';
import { TimeConverter } from 'src/app/core/converters/time-converter';
import { ResultQuestionOptionDto } from 'src/app/models/question-option/result-question-option-dto';

@Component({
    selector: 'app-question-answer',
    templateUrl: './question-answer.component.html',
    styleUrls: ['./question-answer.component.css']
})
export class QuestionAnswerComponent {
    @Input() answer: ResultAnswerDetailDto;

    get answerTimeTakenSeconds() {
        return Math.round(TimeConverter.convertStringTimeToSeconds(this.answer.timeTakenSeconds));
    }

    public findAnswerOption(questionOption: ResultQuestionOptionDto) {
        return this.answer.resultAnswerOptions.find(value => value.optionId === questionOption.id);
    }
}
