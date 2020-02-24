import { Component, OnInit } from '@angular/core';
import { UrlService } from 'src/app/services/url.service';
import { environment } from 'src/environments/environment';
import { MatSnackBar } from '@angular/material';
import { ClipboardSnackBarComponent } from 'src/app/shared/components/clipboard-snack-bar/clipboard-snack-bar.component';
import { UrlsApiDto } from 'src/app/models/url/urls-api-dto';
import { ClipboardManager } from 'src/app/core/clipboard-manager';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-url-list',
    templateUrl: './url-list.component.html',
    styleUrls: ['./url-list.component.css']
})
export class UrlListComponent implements OnInit {
    public columnsToDisplay: string[] = ['id', 'intervieweeName', 'numberOfRuns', 'validFromTime', 'validUntilTime', 'copyLink', 'update'];
    public urlsApi: UrlsApiDto = {} as UrlsApiDto;
    public pageSize = 15;
    public pageSizeOptions: number[] = [this.pageSize, 10, 20];

    constructor(private urlService: UrlService, public snackBar: MatSnackBar, private titleService: Title) { }

    ngOnInit() {
        this.titleService.setTitle('Urls - QuizTest');

        this.setUrlsPage(0, this.pageSize);
    }

    public setUrlsPage(pageIndex: number, pageSize: number) {
        this.urlService.getUrls(pageIndex, pageSize).subscribe(urlsApiResp => this.urlsApi = urlsApiResp.body);
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
