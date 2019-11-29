import { Component, OnInit } from '@angular/core';
import { UrlDto } from 'src/app/models/url/url-dto';
import { UrlService } from 'src/app/services/url.service';
import { environment } from 'src/environments/environment';
import { MatSnackBar } from '@angular/material';
import { ClipboardSnackBarComponent } from 'src/app/shared/components/clipboard-snack-bar/clipboard-snack-bar.component';

@Component({
    selector: 'app-url-list',
    templateUrl: './url-list.component.html',
    styleUrls: ['./url-list.component.css']
})
export class UrlListComponent implements OnInit {
    public columnsToDisplay: string[] = ['id', 'intervieweeName', 'numberOfRuns', 'validFromTime', 'validUntilTime', 'copyLink', 'update'];
    public urls: UrlDto[] = [];

    constructor(private urlService: UrlService, public snackBar: MatSnackBar) { }

    ngOnInit() {
        this.getUrls();
    }

    public copyUrl(urlId: number) {
        const url = environment.baseUrl + urlId;

        const selectedBox = document.createElement('textarea');
        selectedBox.style.position = 'fixed';
        selectedBox.style.left = '0';
        selectedBox.style.top = '0';
        selectedBox.style.opacity = '0';
        selectedBox.value = url;
        document.body.appendChild(selectedBox);
        selectedBox.focus();
        selectedBox.select();
        document.execCommand('copy');
        document.body.removeChild(selectedBox);

        this.openClipboardSnackBar();
    }

    private openClipboardSnackBar() {
        this.snackBar.openFromComponent(ClipboardSnackBarComponent, {
            duration: 10000,
            horizontalPosition: 'start'
        });
    }

    private getUrls() {
        this.urlService.getUrls().subscribe(resp => this.urls = resp.body);
    }
}
