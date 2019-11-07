import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Observable, Subject, Subscription } from 'rxjs';
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
    private subscription: Subscription = new Subscription();
    private urlId: number;

    public viewTest: ViewTestDto = {} as ViewTestDto;
    public sessionId = '';
    public isTestSent = false;

    public timeLimitSeconds: number;
    public timeLimitSecondsCounter = 0;

    @Input() getIdentityUrl$: Observable<IdentityUrlDto>;

    public sendQuestion: Subject<void> = new Subject<void>();

    constructor(private passingTestService: PassingTestService, private testEventService: TestEventService) { }

    ngOnInit() {
        this.subscription.add(
            this.getIdentityUrl$.subscribe(identityUrl => {
                this.urlId = identityUrl.id;

                this.passingTestService.getTestById(identityUrl.testId).subscribe(viewTestResp => {
                    this.viewTest = viewTestResp.body;

                    this.testEventService.generateSessionId().subscribe(sessionIdResp => {
                        this.sessionId = sessionIdResp.body;

                        const newTestEvent = {
                            sessionId: this.sessionId,
                            eventType: EventType.TestStarted,
                            payload: JSON.stringify({
                                testId: this.viewTest.id,
                                intervieweeName: identityUrl.intervieweeName
                            } as PayloadTest)
                        } as NewTestEventDto;

                        this.testEventService.createTestEvent(newTestEvent).subscribe();
                    });

                    this.timeLimitSeconds = TimeConverter.convertStringTimeToSeconds(this.viewTest.timeLimitSeconds);
                });
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    public sendTest() {
        this.sendQuestion.next();

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
