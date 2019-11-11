import { Component, OnInit } from '@angular/core';
import { UrlDto } from 'src/app/models/url/url-dto';
import { UrlService } from 'src/app/services/url.service';

@Component({
    selector: 'app-url-list',
    templateUrl: './url-list.component.html',
    styleUrls: ['./url-list.component.css']
})
export class UrlListComponent implements OnInit {
    public columnsToDisplay: string[] = ['id', 'intervieweeName', 'numberOfRuns', 'validFromTime', 'validUntilTime', 'update'];
    public urls: UrlDto[] = [];

    constructor(private urlService: UrlService) { }

    ngOnInit() {
        this.getUrls();
    }

    private getUrls() {
        this.urlService.getUrls().subscribe(resp => this.urls = resp.body);
    }
}
