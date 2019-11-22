import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { TestService } from 'src/app/services/test.service';
import { TestResultDto } from 'src/app/models/test-result/test-result-dto';

@Component({
    selector: 'app-test-results',
    templateUrl: './test-results.component.html',
    styleUrls: ['./test-results.component.css']
})
export class TestResultsComponent implements OnInit, OnDestroy {
    private subscription: Subscription = new Subscription();

    public columnsToDisplay: string[] = ['id', 'intervieweeName', 'passingStartTime', 'passingEndTime', 'score', 'read'];
    public testResults: TestResultDto[] = [];

    @Input() getTestId$: Observable<number>;

    constructor(private testService: TestService) { }

    ngOnInit() {
        this.subscription.add(
            this.getTestId$.subscribe(testId => {
                this.getResultsByTestId(testId);
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    private getResultsByTestId(testId: number) {
        this.testService.getResultsByTestId(testId).subscribe(resultsResp => {
            this.testResults = resultsResp.body;
        });
    }
}
