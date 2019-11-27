import { Component, OnInit, Input, OnDestroy, Output, EventEmitter } from '@angular/core';
import { Observable, Subject, BehaviorSubject, Subscription } from 'rxjs';
import { ViewQuestionDto } from 'src/app/models/question/view-question-dto';
import { ViewQuestionOptionDto } from 'src/app/models/question-option/view-question-option-dto';
import { TimeConverter } from '../../converters/time-converter';
import { TestEventService } from 'src/app/services/test-event.service';
import { EventType } from 'src/app/models/test-event/enums/event-type.enum';
import { PayloadQuestion } from 'src/app/models/test-event/payloads/payload-question';
import { NewTestEventDto } from 'src/app/models/test-event/new-test-event-dto';

@Component({
    selector: 'app-question-form',
    templateUrl: './question-form.component.html',
    styleUrls: ['./question-form.component.css']
})
export class QuestionFormComponent implements OnInit, OnDestroy {
    private subscription: Subscription = new Subscription();

    public questionTimeLimitSeconds: number;
    public questionTimeLimitSecondsCounter = 0;

    @Input() viewQuestion: ViewQuestionDto;
    @Input() sessionId: string;
    @Input() sendQuestion$: Observable<void>;
    @Output() counterCompleted: EventEmitter<void> = new EventEmitter<void>();

    public getOptions: Subject<void> = new Subject<void>();

    constructor(private testEventService: TestEventService) { }

    ngOnInit() {
        this.questionTimeLimitSeconds = TimeConverter.convertStringTimeToSeconds(this.viewQuestion.timeLimitSeconds);

        this.subscription.add(
            this.sendQuestion$.subscribe(() => {
                this.sendQuestion();
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    public setOption(index: number, option: ViewQuestionOptionDto) {
        this.viewQuestion.testQuestionOptions[index] = option;
    }

    private sendQuestion() {
        this.getOptions.next();

        const selectedOptionsId = this.viewQuestion.testQuestionOptions
            .filter(option => option.isRight === true).map(option => option.id);

        const newTestEvent = {
            sessionId: this.sessionId,
            eventType: EventType.QuestionAnswered,
            payload: JSON.stringify({ questionId: this.viewQuestion.id, selectedOptionsId } as PayloadQuestion)
        } as NewTestEventDto;

        this.testEventService.createTestEvent(newTestEvent).subscribe();
    }
}
