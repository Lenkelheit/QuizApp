import { Component, OnInit, Input, OnDestroy, ViewChild, Output, EventEmitter } from '@angular/core';
import { Observable, Subject, BehaviorSubject } from 'rxjs';
import { ViewQuestionDto } from 'src/app/models/passing-test/view-question-dto';
import { takeUntil } from 'rxjs/operators';
import { ViewQuestionOptionDto } from 'src/app/models/passing-test/view-question-option-dto';
import { TimeConverter } from '../../converters/time-converter';
import { TestEventService } from 'src/app/services/test-event.service';
import { EventType } from 'src/app/models/test-event/enums/event-type.enum';
import { PayloadQuestion } from 'src/app/models/test-event/payloads/payload-question';
import { NewTestEventDto } from 'src/app/models/test-event/new-test-event-dto';
import { MatStepper } from '@angular/material';

@Component({
    selector: 'app-questions-form',
    templateUrl: './questions-form.component.html',
    styleUrls: ['./questions-form.component.css']
})
export class QuestionsFormComponent implements OnInit, OnDestroy {
    public viewQuestions: ViewQuestionDto[] = [];

    private questionsTimeLimitSeconds: number[] = [];
    private questionsTimeLimitSecondsCounters: number[] = [];

    @ViewChild('stepper', { static: false }) stepper: MatStepper;
    @Input() sessionId: string;
    @Input() initializeQuestions$: Observable<ViewQuestionDto[]>;
    @Input() sendLastSelectedQuestion$: Observable<void>;
    @Output() passUpSendTest: EventEmitter<void> = new EventEmitter<void>();

    private getOptions: Subject<void>[] = [];
    private initializeQuestionOptions: BehaviorSubject<ViewQuestionOptionDto[]>[] = [];
    private ngUnsubscribe = new Subject();

    constructor(private testEventService: TestEventService) { }

    ngOnInit() {
        this.initializeQuestions$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(questions => {
            this.viewQuestions = questions;

            this.viewQuestions.forEach((value, index) => {
                this.getOptions.push(new Subject<void>());

                this.questionsTimeLimitSeconds.push(TimeConverter.convertStringTimeToSeconds(this.viewQuestions[index].timeLimitSeconds));

                this.initializeQuestionOptions
                    .push(new BehaviorSubject<ViewQuestionOptionDto[]>(this.viewQuestions[index].testQuestionOptions));
            });
        });

        this.sendLastSelectedQuestion$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.sendQuestion(this.stepper.selectedIndex);
        });
    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public sendQuestion(index: number) {
        this.getOptions[index].next();

        const selectedOptionsId = this.viewQuestions[index].testQuestionOptions
            .filter(option => option.isRight === true).map(option => option.id);

        const newTestEvent = {
            sessionId: this.sessionId,
            eventType: EventType.QuestionAnswered,
            payload: JSON.stringify({ questionId: this.viewQuestions[index].id, selectedOptionsId } as PayloadQuestion)
        } as NewTestEventDto;

        this.testEventService.createTestEvent(newTestEvent).subscribe();
    }

    private saveOptions(index: number, options: ViewQuestionOptionDto[]) {
        this.viewQuestions[index].testQuestionOptions = options;
    }
}
