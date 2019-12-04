import { Component, OnInit, Input } from '@angular/core';
import { ResultQuestionOptionDto } from 'src/app/models/question-option/result-question-option-dto';
import { ResultAnswerOptionDetailDto } from 'src/app/models/result-answer-option/result-answer-option-detail-dto';

@Component({
    selector: 'app-question-option',
    templateUrl: './question-option.component.html',
    styleUrls: ['./question-option.component.css']
})
export class QuestionOptionComponent {
    @Input() questionOption: ResultQuestionOptionDto;
    @Input() answerOption: ResultAnswerOptionDetailDto;
}
