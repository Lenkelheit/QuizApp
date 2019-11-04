import { Component, OnInit, Input, OnDestroy, EventEmitter, Output } from '@angular/core';
import { ViewQuestionOptionDto } from 'src/app/models/passing-test/view-question-option-dto';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
    selector: 'app-question-options-form',
    templateUrl: './question-options-form.component.html',
    styleUrls: ['./question-options-form.component.css']
})
export class QuestionOptionsFormComponent implements OnInit, OnDestroy {
    public viewQuestionOptions: ViewQuestionOptionDto[] = [];

    @Input() initializeQuestionOptions$: Observable<ViewQuestionOptionDto[]>;
    @Input() getOptions$: Observable<void>;
    @Output() passUpOptions: EventEmitter<ViewQuestionOptionDto[]> = new EventEmitter<ViewQuestionOptionDto[]>();

    private ngUnsubscribe = new Subject();

    constructor() { }

    ngOnInit() {
        this.initializeQuestionOptions$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(options => {
            this.viewQuestionOptions = options;
        });

        this.getOptions$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.viewQuestionOptions.forEach(option => {
                if (typeof option.isRight === 'undefined') {
                    option.isRight = false;
                }
            });

            this.passUpOptions.emit(this.viewQuestionOptions);
        });
    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
