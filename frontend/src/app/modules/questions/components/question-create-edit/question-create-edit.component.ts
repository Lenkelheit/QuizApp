import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, Subject, merge, Subscription } from 'rxjs';
import { ValidationRegexes } from 'src/app/core/validators/validation-regexes';
import { ValidControlMatcher } from 'src/app/core/error-state-matchers/valid-control-matcher';
import { UpdateQuestionDto } from 'src/app/models/question/update-question-dto';
import { UpdateQuestionOptionDto } from 'src/app/models/question-option/update-question-option-dto';
import { FormatTimeLimitValidator } from 'src/app/core/validators/format-time-limit-validator';

@Component({
    selector: 'app-question-create-edit',
    templateUrl: './question-create-edit.component.html',
    styleUrls: ['./question-create-edit.component.css']
})
export class QuestionCreateEditComponent implements OnInit, OnDestroy {
    private subscription: Subscription = new Subscription();
    private questionOptionsFormStatusInvalid: boolean[] = [];

    @Input() updateQuestion: UpdateQuestionDto;
    @Input() getQuestion$: Observable<void>;
    @Output() passUpUpdateQuestion: EventEmitter<UpdateQuestionDto> = new EventEmitter<UpdateQuestionDto>();
    @Output() passUpQuestionFormStatusInvalid: EventEmitter<boolean> = new EventEmitter<boolean>();
    @Output() deleteQuestion: EventEmitter<void> = new EventEmitter<void>();

    public getOptions: Subject<void> = new Subject<void>();
    public updateQuestionOptionsFormStatus: Subject<void> = new Subject<void>();
    public questionAndOptionsFormStatusChanges$: Observable<void>;

    public questionForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.questionForm = this.formBuilder.group({
            text: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(256)]],
            hint: ['', [Validators.minLength(4), Validators.maxLength(256)]],
            timeLimitSeconds: ['', [Validators.required, FormatTimeLimitValidator.validate(ValidationRegexes.timeLimitRegex)]]
        });

        this.updateQuestion.testQuestionOptions.forEach(() => {
            this.questionOptionsFormStatusInvalid.push(false);
        });

        this.questionAndOptionsFormStatusChanges$ =
            merge(this.questionForm.statusChanges, this.updateQuestionOptionsFormStatus.asObservable());

        this.subscription.add(
            this.questionAndOptionsFormStatusChanges$.subscribe(() => {
                const questionOptionsFormStatusInvalid = this.questionOptionsFormStatusInvalid.some(value => value === true);

                this.passUpQuestionFormStatusInvalid.emit((this.questionForm.status === 'INVALID') || questionOptionsFormStatusInvalid);
            })
        );

        this.subscription.add(
            this.getQuestion$.subscribe(() => {
                this.getOptions.next();
                this.passUpUpdateQuestion.emit(this.updateQuestion);
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    get text() {
        return this.questionForm.get('text');
    }

    get hint() {
        return this.questionForm.get('hint');
    }

    get timeLimitSeconds() {
        return this.questionForm.get('timeLimitSeconds');
    }

    public addQuestionOption() {
        this.updateQuestion.testQuestionOptions.push({} as UpdateQuestionOptionDto);
        this.questionOptionsFormStatusInvalid.push(true);

        this.updateQuestionOptionsFormStatus.next();
    }

    public deleteQuestionOption(index: number) {
        this.updateQuestion.testQuestionOptions.splice(index, 1);
        this.questionOptionsFormStatusInvalid.splice(index, 1);

        this.updateQuestionOptionsFormStatus.next();

        if (this.updateQuestion.testQuestionOptions.length === 0) {
            this.addQuestionOption();
        }
    }

    public setQuestionOption(index: number, updateQuestionOption: UpdateQuestionOptionDto) {
        this.updateQuestion.testQuestionOptions[index] = updateQuestionOption;
    }

    public setQuestionOptionFormStatusInvalid(index: number, questionOptionFormStatusInvalid: boolean) {
        this.questionOptionsFormStatusInvalid[index] = questionOptionFormStatusInvalid;

        this.updateQuestionOptionsFormStatus.next();
    }
}
