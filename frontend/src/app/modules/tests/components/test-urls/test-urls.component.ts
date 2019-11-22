import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { UrlDto } from 'src/app/models/url/url-dto';
import { TestService } from 'src/app/services/test.service';
import { environment } from 'src/environments/environment';
import { Observable, Subject, Subscription } from 'rxjs';

@Component({
    selector: 'app-test-urls',
    templateUrl: './test-urls.component.html',
    styleUrls: ['./test-urls.component.css']
})
export class TestUrlsComponent implements OnInit, OnDestroy {
    private subscription: Subscription = new Subscription();

    public columnsToDisplay: string[] = ['id', 'intervieweeName', 'numberOfRuns', 'validFromTime', 'validUntilTime', 'urlId', 'update'];
    public testUrls: UrlDto[] = [];

    public baseUrl: string = environment.baseUrl;

    @Input() getTestId$: Observable<number>;

    constructor(private testService: TestService) { }

    ngOnInit() {
        this.subscription.add(
            this.getTestId$.subscribe(testId => {
                this.getUrlsByTestId(testId);
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    private getUrlsByTestId(testId: number) {
        this.testService.getUrlsByTestId(testId).subscribe(urlsResp => {
            this.testUrls = urlsResp.body;
        });
    }
}
