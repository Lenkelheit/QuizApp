import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, Subject, merge, BehaviorSubject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ValidationRegexes } from 'src/app/shared/validators/validation-regexes';
import { ValidControlMatcher } from 'src/app/shared/error-state-matchers/valid-control-matcher';
import { UpdateQuestionDto } from 'src/app/models/question/update-question-dto';
import { UpdateQuestionOptionDto } from 'src/app/models/question-option/update-question-option-dto';
import { FormatTimeLimitValidator } from 'src/app/shared/validators/format-time-limit-validator';

@Component({
    selector: 'app-question-create-edit',
    templateUrl: './question-create-edit.component.html',
    styleUrls: ['./question-create-edit.component.css']
})
export class QuestionCreateEditComponent implements OnInit, OnDestroy {
    public updateQuestions: UpdateQuestionDto[] = [];

    public questionOptionsFormsStatusInvalid: boolean[] = [];

    @Input() initializeQuestions$: Observable<UpdateQuestionDto[]>;
    @Input() deleteQuestionsForms$: Observable<void>;
    @Input() getQuestions$: Observable<void>;
    @Output() passUpUpdateQuestions: EventEmitter<UpdateQuestionDto[]> = new EventEmitter<UpdateQuestionDto[]>();
    @Output() passUpQuestionsFormStatusInvalid: EventEmitter<boolean> = new EventEmitter<boolean>();

    private getOptions: Subject<void>[] = [];
    private deleteOptionsForms: Subject<void> = new Subject<void>();
    private updateQuestionOptionsFormStatus: Subject<void> = new Subject<void>();
    private questionsAndOptionsFormsStatusChanges$: Observable<void>;
    private initializeQuestionOptions: BehaviorSubject<UpdateQuestionOptionDto[]>[] = [];
    private ngUnsubscribe = new Subject();

    public questionsForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.questionsForm = this.formBuilder.group({
            questions: this.formBuilder.array([])
        });

        this.initializeQuestions$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(questions => {
            if (questions.length === 0) {

                this.updateQuestions = [{} as UpdateQuestionDto];
                this.questionOptionsFormsStatusInvalid.push(true);

                this.getOptions.push(new Subject<void>());
                this.initializeQuestionOptions.push(new BehaviorSubject<UpdateQuestionOptionDto[]>([{} as UpdateQuestionOptionDto]));

                this.questions.push(this.addQuestionFormGroup());
            } else {
                this.updateQuestions = questions;
                this.questionOptionsFormsStatusInvalid.push(false);

                this.updateQuestions.forEach((value, index) => {
                    this.getOptions.push(new Subject<void>());
                    this.initializeQuestionOptions
                        .push(new BehaviorSubject<UpdateQuestionOptionDto[]>(this.updateQuestions[index].testQuestionOptions));

                    this.questions.push(this.addQuestionFormGroup());
                });
            }
        });

        this.questionsAndOptionsFormsStatusChanges$ =
            merge(this.questionsForm.statusChanges, this.updateQuestionOptionsFormStatus.asObservable());

        this.questionsAndOptionsFormsStatusChanges$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            const questionOptionsFormsStatusInvalid = this.questionOptionsFormsStatusInvalid.some(value => value === true);

            this.passUpQuestionsFormStatusInvalid.emit((this.questionsForm.status === 'INVALID') || questionOptionsFormsStatusInvalid);
        });

        this.getQuestions$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.getOptions.forEach(elem => elem.next());

            this.passUpUpdateQuestions.emit(this.updateQuestions);
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
        this.updateQuestions.push({} as UpdateQuestionDto);

        this.getOptions.push(new Subject<void>());
        this.initializeQuestionOptions.push(new BehaviorSubject<UpdateQuestionOptionDto[]>([{} as UpdateQuestionOptionDto]));

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
        this.updateQuestions.splice(index, 1);
        this.questions.removeAt(index);

        this.getOptions.splice(index, 1);
        this.initializeQuestionOptions.splice(index, 1);

        this.questionOptionsFormsStatusInvalid.splice(index, 1);

        if (this.updateQuestions.length === 0) {
            this.clearQuestions();
        }
    }

    private clearQuestions() {
        this.updateQuestions = [{} as UpdateQuestionDto];
        this.questions.clear();
        this.questions.push(this.addQuestionFormGroup());

        this.getOptions = [new Subject<void>()];
        this.initializeQuestionOptions = [new BehaviorSubject<UpdateQuestionOptionDto[]>([{} as UpdateQuestionOptionDto])];

        this.questionOptionsFormsStatusInvalid = [true];
    }

    private clearQuestionsWithChildForms() {
        this.clearQuestions();
        this.deleteOptionsForms.next();
    }

    private saveQuestionOptions(index: number, updateQuestionOptions: UpdateQuestionOptionDto[]) {
        this.updateQuestions[index].testQuestionOptions = updateQuestionOptions;
    }

    private saveQuestionOptionsFormStatusInvalid(index: number, questionOptionsFormStatusInvalid: boolean) {
        this.questionOptionsFormsStatusInvalid[index] = questionOptionsFormStatusInvalid;

        this.updateQuestionOptionsFormStatus.next();
    }
}
