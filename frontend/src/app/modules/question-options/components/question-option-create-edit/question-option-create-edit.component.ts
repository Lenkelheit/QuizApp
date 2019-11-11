import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, Subject, Subscription } from 'rxjs';
import { ValidControlMatcher } from 'src/app/core/error-state-matchers/valid-control-matcher';
import { UpdateQuestionOptionDto } from 'src/app/models/question-option/update-question-option-dto';

@Component({
    selector: 'app-question-option-create-edit',
    templateUrl: './question-option-create-edit.component.html',
    styleUrls: ['./question-option-create-edit.component.css']
})
export class QuestionOptionCreateEditComponent implements OnInit, OnDestroy {
    @Input() updateQuestionOption: UpdateQuestionOptionDto;

    @Input() getOption$: Observable<void>;
    @Output() passUpUpdateQuestionOption: EventEmitter<UpdateQuestionOptionDto> = new EventEmitter<UpdateQuestionOptionDto>();
    @Output() passUpQuestionOptionFormStatusInvalid: EventEmitter<boolean> = new EventEmitter<boolean>();
    @Output() deleteQuestionOption: EventEmitter<void> = new EventEmitter<void>();

    public questionOptionForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    private subscription: Subscription = new Subscription();

    constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.questionOptionForm = this.formBuilder.group({
            text: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(256)]],
            isRight: ['']
        });

        this.questionOptionForm.statusChanges.subscribe(status => {
            this.passUpQuestionOptionFormStatusInvalid.emit(status === 'INVALID');
        });

        this.subscription.add(
            this.getOption$.subscribe(() => {
                if (typeof this.updateQuestionOption.isRight === 'undefined') {
                    this.updateQuestionOption.isRight = false;
                }

                this.passUpUpdateQuestionOption.emit(this.updateQuestionOption);
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    get text() {
        return this.questionOptionForm.get('text');
    }
}
