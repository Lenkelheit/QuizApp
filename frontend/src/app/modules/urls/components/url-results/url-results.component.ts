import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { UrlService } from 'src/app/services/url.service';
import { TestResultsApiDto } from 'src/app/models/test-result/test-results-api-dto';

@Component({
    selector: 'app-url-results',
    templateUrl: './url-results.component.html',
    styleUrls: ['./url-results.component.css']
})
export class UrlResultsComponent implements OnInit, OnDestroy {
    private subscription: Subscription = new Subscription();

    public columnsToDisplay: string[] = ['id', 'intervieweeName', 'passingStartTime', 'passingEndTime', 'score', 'read'];
    public testResultsApi: TestResultsApiDto = {} as TestResultsApiDto;
    public pageSize = 15;
    public pageSizeOptions: number[] = [this.pageSize, 10, 20];
    public urlId: number;

    @Input() getUrlId$: Observable<number>;

    constructor(private urlService: UrlService) { }

    ngOnInit() {
        this.subscription.add(
            this.getUrlId$.subscribe(urlId => {
                this.urlId = urlId;
                this.setTestResultsPage(urlId, 0, this.pageSize);
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    public setTestResultsPage(urlId: number, pageIndex: number, pageSize: number) {
        this.urlService.getTestResultsByUrlId(urlId, pageIndex, pageSize).subscribe(resultsApiResp => {
            this.testResultsApi = resultsApiResp.body;
        });
    }
}
