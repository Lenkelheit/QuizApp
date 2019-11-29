import { Component, OnInit, Input, OnDestroy, EventEmitter, Output } from '@angular/core';
import { ViewQuestionOptionDto } from 'src/app/models/question-option/view-question-option-dto';
import { Observable, Subject, Subscription } from 'rxjs';

@Component({
    selector: 'app-question-option-form',
    templateUrl: './question-option-form.component.html',
    styleUrls: ['./question-option-form.component.css']
})
export class QuestionOptionFormComponent implements OnInit, OnDestroy {
    private subscription: Subscription = new Subscription();

    @Input() viewQuestionOption: ViewQuestionOptionDto;
    @Input() getOption$: Observable<void>;
    @Output() passUpOption: EventEmitter<ViewQuestionOptionDto> = new EventEmitter<ViewQuestionOptionDto>();

    ngOnInit() {
        this.subscription.add(
            this.getOption$.subscribe(() => {
                if (typeof this.viewQuestionOption.isRight === 'undefined') {
                    this.viewQuestionOption.isRight = false;
                }

                this.passUpOption.emit(this.viewQuestionOption);
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
