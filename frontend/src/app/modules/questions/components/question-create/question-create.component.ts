import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, Subject, merge } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NewQuestionDto } from 'src/app/models/question/new-question-dto';
import { CreatedQuestionDto } from 'src/app/models/question/created-question-dto';
import { NewQuestionOptionDto } from 'src/app/models/question-option/new-question-option-dto';
import { FormatTimeLimitValidator } from 'src/app/shared/validators/format-time-limit-validator';
import { ValidationRegexes } from 'src/app/shared/validators/validation-regexes';
import { ValidControlMatcher } from 'src/app/shared/error-state-matchers/valid-control-matcher';

@Component({
    selector: 'app-question-create',
    templateUrl: './question-create.component.html',
    styleUrls: ['./question-create.component.css']
})
export class QuestionCreateComponent implements OnInit, OnDestroy {
    public newQuestions: NewQuestionDto[] = [{} as NewQuestionDto];
    public createdQuestions: CreatedQuestionDto[] = [];

    public questionOptionsFormsStatusInvalid: boolean[] = [true];

    @Input() deleteQuestionsForms$: Observable<void>;
    @Input() getQuestionsAndDeleteForms$: Observable<void>;
    @Output() passUpNewQuestions: EventEmitter<NewQuestionDto[]> = new EventEmitter<NewQuestionDto[]>();
    @Output() passUpQuestionsFormStatusInvalid: EventEmitter<boolean> = new EventEmitter<boolean>();

    private getOptionsAndDeleteForms: Subject<void>[] = [new Subject<void>()];
    private deleteOptionsForms: Subject<void> = new Subject<void>();
    private updateQuestionOptionsFormStatus: Subject<void> = new Subject<void>();
    private questionsAndOptionsFormsStatusChanges$: Observable<void>;
    private ngUnsubscribe = new Subject();

    public questionsForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.questionsForm = this.formBuilder.group({
            questions: this.formBuilder.array([
                this.addQuestionFormGroup()
            ])
        });

        this.questionsAndOptionsFormsStatusChanges$ =
            merge(this.questionsForm.statusChanges, this.updateQuestionOptionsFormStatus.asObservable());

        this.questionsAndOptionsFormsStatusChanges$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            const questionOptionsFormsStatusInvalid = this.questionOptionsFormsStatusInvalid.some(value => value === true);

            this.passUpQuestionsFormStatusInvalid.emit((this.questionsForm.status === 'INVALID') || questionOptionsFormsStatusInvalid);
        });

        this.getQuestionsAndDeleteForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.getOptionsAndDeleteForms.forEach(elem => elem.next());

            this.passUpNewQuestions.emit(this.newQuestions);

            this.clearQuestions();
        });

        this.deleteQuestionsForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.clearQuestionsWithChildForms();
        });
    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    get questions() {
        return this.questionsForm.get('questions') as FormArray;
    }

    public getQuestion(index: number) {
        return this.questions.controls[index] as FormGroup;
    }

    public getText(index: number) {
        return this.getQuestion(index).controls.text;
    }

    public getHint(index: number) {
        return this.getQuestion(index).controls.hint;
    }

    public getTimeLimitSeconds(index: number) {
        return this.getQuestion(index).controls.timeLimitSeconds;
    }

    public addQuestion() {
        this.questions.push(this.addQuestionFormGroup());
        this.newQuestions.push({} as NewQuestionDto);

        this.getOptionsAndDeleteForms.push(new Subject<void>());

        this.questionOptionsFormsStatusInvalid.push(true);
    }

    public addQuestionFormGroup() {
        return this.formBuilder.group({
            text: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(256)]],
            hint: ['', [Validators.minLength(4), Validators.maxLength(256)]],
            timeLimitSeconds: ['', [Validators.required, FormatTimeLimitValidator.validate(ValidationRegexes.timeLimitRegex)]]
        });
    }

    public deleteQuestion(index: number) {
        this.newQuestions.splice(index, 1);

        this.questions.removeAt(index);

        this.getOptionsAndDeleteForms.splice(index, 1);

        this.createdQuestions.splice(index, 1);

        this.questionOptionsFormsStatusInvalid.splice(index, 1);

        if (this.newQuestions.length === 0) {
            this.clearQuestions();
        }
    }

    private clearQuestions() {
        this.newQuestions = [{} as NewQuestionDto];
        this.createdQuestions = [];
        this.questions.clear();
        this.questions.push(this.addQuestionFormGroup());
        this.getOptionsAndDeleteForms = [new Subject<void>()];
        this.questionOptionsFormsStatusInvalid = [true];
    }

    private clearQuestionsWithChildForms() {
        this.clearQuestions();
        this.deleteOptionsForms.next();
    }

    private saveQuestionOptions(index: number, newQuestionOptions: NewQuestionOptionDto[]) {
        this.newQuestions[index].testQuestionOptions = newQuestionOptions;
    }

    private saveQuestionOptionsFormStatusInvalid(index: number, questionOptionsFormStatusInvalid: boolean) {
        this.questionOptionsFormsStatusInvalid[index] = questionOptionsFormStatusInvalid;

        this.updateQuestionOptionsFormStatus.next();
    }
}
