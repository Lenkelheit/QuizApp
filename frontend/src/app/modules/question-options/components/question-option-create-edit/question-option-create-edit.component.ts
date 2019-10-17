import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ValidControlMatcher } from 'src/app/shared/error-state-matchers/valid-control-matcher';
import { UpdateQuestionOptionDto } from 'src/app/models/question-option/update-question-option-dto';

@Component({
    selector: 'app-question-option-create-edit',
    templateUrl: './question-option-create-edit.component.html',
    styleUrls: ['./question-option-create-edit.component.css']
})
export class QuestionOptionCreateEditComponent implements OnInit, OnDestroy {
    public updateQuestionOptions: UpdateQuestionOptionDto[] = [];

    @Input() initializeQuestionOptions$: Observable<UpdateQuestionOptionDto[]>;
    @Input() deleteOptionsForms$: Observable<void>;
    @Input() getOptions$: Observable<void>;
    @Output() passUpUpdateQuestionOptions: EventEmitter<UpdateQuestionOptionDto[]> = new EventEmitter<UpdateQuestionOptionDto[]>();
    @Output() passUpQuestionOptionsFormStatusInvalid: EventEmitter<boolean> = new EventEmitter<boolean>();
    private ngUnsubscribe = new Subject();

    questionOptionsForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.questionOptionsForm = this.formBuilder.group({
            questionOptions: this.formBuilder.array([])
        });

        this.initializeQuestionOptions$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(options => {
            this.updateQuestionOptions = options;

            this.updateQuestionOptions.forEach((value, index) => {
                this.questionOptions.push(this.addQuestionOptionFormGroup());
            });
        });

        this.questionOptionsForm.statusChanges.subscribe(status => {
            this.passUpQuestionOptionsFormStatusInvalid.emit(status === 'INVALID');
        });

        this.getOptions$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(question => {
            this.updateQuestionOptions.forEach(option => {
                if (typeof option.isRight === 'undefined') {
                    option.isRight = false;
                }
            });

            this.passUpUpdateQuestionOptions.emit(this.updateQuestionOptions);
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

        this.updateQuestionOptions.push({} as UpdateQuestionOptionDto);
    }

    public addQuestionOptionFormGroup() {
        return this.formBuilder.group({
            text: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(256)]],
            isRight: ['']
        });
    }

    public deleteQuestionOption(index: number) {
        this.updateQuestionOptions.splice(index, 1);
        this.questionOptions.removeAt(index);

        if (this.updateQuestionOptions.length === 0) {
            this.clearOptions();
        }
    }

    private clearOptions() {
        this.updateQuestionOptions = [{} as UpdateQuestionOptionDto];
        this.questionOptions.clear();
        this.questionOptions.push(this.addQuestionOptionFormGroup());
    }
}
