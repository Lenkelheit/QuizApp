import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { TestService } from 'src/app/services/test.service';
import { environment } from 'src/environments/environment';
import { Observable, Subject, Subscription } from 'rxjs';
import { UrlsApiDto } from 'src/app/models/url/urls-api-dto';
import { ClipboardManager } from 'src/app/core/clipboard-manager';
import { MatSnackBar } from '@angular/material';
import { ClipboardSnackBarComponent } from 'src/app/shared/components/clipboard-snack-bar/clipboard-snack-bar.component';

@Component({
    selector: 'app-test-urls',
    templateUrl: './test-urls.component.html',
    styleUrls: ['./test-urls.component.css']
})
export class TestUrlsComponent implements OnInit, OnDestroy {
    private subscription: Subscription = new Subscription();

    public columnsToDisplay: string[] =
        ['id', 'intervieweeName', 'numberOfRuns', 'validFromTime', 'validUntilTime', 'urlId', 'copyLink', 'update'];
    public urlsApi: UrlsApiDto = {} as UrlsApiDto;
    public pageSize = 15;
    public pageSizeOptions: number[] = [this.pageSize, 10, 20];
    public testId: number;

    public baseUrl: string = environment.baseUrl;

    @Input() getTestId$: Observable<number>;

    constructor(private testService: TestService, public snackBar: MatSnackBar) { }

    ngOnInit() {
        this.subscription.add(
            this.getTestId$.subscribe(testId => {
                this.testId = testId;
                this.setUrlsPage(testId, 0, this.pageSize);
            })
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    public setUrlsPage(testId: number, pageIndex: number, pageSize: number) {
        this.testService.getUrlsByTestId(testId, pageIndex, pageSize).subscribe(urlsApiResp => {
            this.urlsApi = urlsApiResp.body;
        });
    }

    public copyUrl(urlId: number) {
        const url = environment.baseUrl + urlId;

        ClipboardManager.copyUrl(url);
        this.openClipboardSnackBar();
    }

    private openClipboardSnackBar() {
        this.snackBar.openFromComponent(ClipboardSnackBarComponent, {
            duration: 10000,
            horizontalPosition: 'start'
        });
    }
}
