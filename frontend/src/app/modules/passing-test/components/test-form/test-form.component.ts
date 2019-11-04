import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Observable, Subject, timer } from 'rxjs';
import { takeUntil, take, map } from 'rxjs/operators';
import { PassingTestService } from 'src/app/services/passing-test.service';
import { ViewTestDto } from 'src/app/models/passing-test/view-test-dto';
import { ViewQuestionDto } from 'src/app/models/passing-test/view-question-dto';
import { TimeConverter } from '../../converters/time-converter';
import { TestEventService } from 'src/app/services/test-event.service';
import { NewTestEventDto } from 'src/app/models/test-event/new-test-event-dto';
import { EventType } from 'src/app/models/test-event/enums/event-type.enum';
import { PayloadTest } from 'src/app/models/test-event/payloads/payload-test';
import { IdentityUrlDto } from 'src/app/models/passing-test/identity-url-dto';
import { UserUrlDto } from 'src/app/models/passing-test/user-url-dto';

@Component({
    selector: 'app-test-form',
    templateUrl: './test-form.component.html',
    styleUrls: ['./test-form.component.css']
})
export class TestFormComponent implements OnInit, OnDestroy {
    public viewTest: ViewTestDto = {} as ViewTestDto;
    public sessionId = '';
    public isTestSent = false;
    private urlId: number;

    private timeLimitSeconds: number;
    private timeLimitSecondsCounter = 0;

    @Input() getIdentityUrl$: Observable<IdentityUrlDto>;

    private initializeQuestions: Subject<ViewQuestionDto[]> = new Subject<ViewQuestionDto[]>();
    private sendLastSelectedQuestion: Subject<void> = new Subject<void>();
    private ngUnsubscribe = new Subject();

    constructor(private passingTestService: PassingTestService, private testEventService: TestEventService) { }

    ngOnInit() {
        this.getIdentityUrl$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(identityUrl => {
            this.urlId = identityUrl.id;

            this.passingTestService.getTestById(identityUrl.testId).subscribe(viewTestResp => {
                this.viewTest = viewTestResp.body;

                this.testEventService.generateSessionId().subscribe(sessionIdResp => {
                    this.sessionId = sessionIdResp.body;

                    const newTestEvent = {
                        sessionId: this.sessionId,
                        eventType: EventType.TestStarted,
                        payload: JSON.stringify({ testId: this.viewTest.id, intervieweeName: identityUrl.intervieweeName } as PayloadTest)
                    } as NewTestEventDto;

                    this.testEventService.createTestEvent(newTestEvent).subscribe();
                });

                this.timeLimitSeconds = TimeConverter.convertStringTimeToSeconds(this.viewTest.timeLimitSeconds);

                this.initializeQuestions.next(this.viewTest.testQuestions);
            });
        });
    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public sendTest() {
        this.sendLastSelectedQuestion.next();

        const userUrl = {
            urlId: this.urlId,
            sessionId: this.sessionId,
        } as UserUrlDto;
        this.passingTestService.createTestResult(userUrl).subscribe(createdTestResultDtoResp => {
            console.log(createdTestResultDtoResp.body);
        });

        this.isTestSent = true;
    }
}
