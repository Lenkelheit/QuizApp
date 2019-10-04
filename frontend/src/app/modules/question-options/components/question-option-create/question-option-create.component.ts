import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NewQuestionOptionDto } from 'src/app/models/question-option/new-question-option-dto';
import { CreatedQuestionDto } from 'src/app/models/question/created-question-dto';
import { CreatedQuestionOptionDto } from 'src/app/models/question-option/created-question-option-dto';
import { ValidControlMatcher } from 'src/app/shared/error-state-matchers/valid-control-matcher';

@Component({
    selector: 'app-question-option-create',
    templateUrl: './question-option-create.component.html',
    styleUrls: ['./question-option-create.component.css']
})
export class QuestionOptionCreateComponent implements OnInit, OnDestroy {
    public newQuestionOptions: NewQuestionOptionDto[] = [{} as NewQuestionOptionDto];
    public createdQuestionOptions: CreatedQuestionOptionDto[] = [];

    @Input() deleteOptionsForms$: Observable<void>;
    @Input() getOptionsAndDeleteForms$: Observable<void>;
    @Output() passUpNewQuestionOptions: EventEmitter<NewQuestionOptionDto[]> = new EventEmitter<NewQuestionOptionDto[]>();
    @Output() passUpQuestionOptionsFormStatusInvalid: EventEmitter<boolean> = new EventEmitter<boolean>();
    private ngUnsubscribe = new Subject();

    questionOptionsForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.questionOptionsForm = this.formBuilder.group({
            questionOptions: this.formBuilder.array([
                this.addQuestionOptionFormGroup()
            ])
        });

        this.questionOptionsForm.statusChanges.subscribe(status => {
            this.passUpQuestionOptionsFormStatusInvalid.emit(status === 'INVALID');
        });

        this.getOptionsAndDeleteForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(question => {
            this.newQuestionOptions.forEach(option => {
                if (typeof option.isRight === 'undefined') {
                    option.isRight = false;
                }
            });

            this.passUpNewQuestionOptions.emit(this.newQuestionOptions);
            this.clearOptions();
        });

        this.deleteOptionsForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.clearOptions();
        });
    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    get questionOptions() {
        return this.questionOptionsForm.get('questionOptions') as FormArray;
    }

    public getQuestionOption(index: number) {
        return this.questionOptions.controls[index] as FormGroup;
    }

    public getText(index: number) {
        return this.getQuestionOption(index).controls.text;
    }

    public addQuestionOption() {
        this.questionOptions.push(this.addQuestionOptionFormGroup());

        this.newQuestionOptions.push({} as NewQuestionOptionDto);
    }

    public addQuestionOptionFormGroup() {
        return this.formBuilder.group({
            text: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(256)]],
            isRight: ['']
        });
    }

    public deleteQuestionOption(index: number) {
        this.newQuestionOptions.splice(index, 1);
        this.questionOptions.removeAt(index);

        if (this.newQuestionOptions.length === 0) {
            this.clearOptions();
        }
    }

    private clearOptions() {
        this.newQuestionOptions = [{} as NewQuestionOptionDto];
        this.createdQuestionOptions = [];
        this.questionOptions.clear();
        this.questionOptions.push(this.addQuestionOptionFormGroup());
    }
}
