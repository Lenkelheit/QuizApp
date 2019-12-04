import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { TestService } from 'src/app/services/test.service';
import { TestResultsApiDto } from 'src/app/models/test-result/test-results-api-dto';

@Component({
    selector: 'app-test-results',
    templateUrl: './test-results.component.html',
    styleUrls: ['./test-results.component.css']
})
export class TestResultsComponent implements OnInit, OnDestroy {
    private subscription: Subscription = new Subscription();

    public columnsToDisplay: string[] = ['id', 'intervieweeName', 'passingStartTime', 'passingEndTime', 'score', 'read'];
    public testResultsApi: TestResultsApiDto = {} as TestResultsApiDto;
    public pageSize = 15;
    public pageSizeOptions: number[] = [this.pageSize, 10, 20];
    public testId: number;

    @Input() getTestId$: Observable<number>;

    constructor(private testService: TestService) { }

    ngOnInit() {
        this.subscription.add(
            this.getTestId$.subscribe(testId => {
                this.testId = testId;
                this.setResultsPage(testId, 0, this.pageSize);
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    public setResultsPage(testId: number, pageIndex: number, pageSize: number) {
        this.testService.getResultsByTestId(testId, pageIndex, pageSize).subscribe(resultsApiResp => {
            this.testResultsApi = resultsApiResp.body;
        });
    }
}
