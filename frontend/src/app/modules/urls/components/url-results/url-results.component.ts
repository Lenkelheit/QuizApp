import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { TestResultDto } from 'src/app/models/test-result/test-result-dto';
import { UrlService } from 'src/app/services/url.service';

@Component({
    selector: 'app-url-results',
    templateUrl: './url-results.component.html',
    styleUrls: ['./url-results.component.css']
})
export class UrlResultsComponent implements OnInit, OnDestroy {
    private subscription: Subscription = new Subscription();

    public columnsToDisplay: string[] = ['intervieweeName', 'passingStartTime', 'passingEndTime', 'score', 'read'];
    public testResultsForUrl: TestResultDto[] = [];

    @Input() getUrlId$: Observable<number>;

    constructor(private urlService: UrlService) { }

    ngOnInit() {
        this.subscription.add(
            this.getUrlId$.subscribe(urlId => {
                this.getTestResultsByUrlId(urlId);
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    private getTestResultsByUrlId(urlId: number) {
        this.urlService.getTestResultsByUrlId(urlId).subscribe(resultsResp => {
            this.testResultsForUrl = resultsResp.body;
        });
    }
}
