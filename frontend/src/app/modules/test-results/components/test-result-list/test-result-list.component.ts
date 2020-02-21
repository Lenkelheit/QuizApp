import { Component, OnInit } from '@angular/core';
import { TestResultService } from 'src/app/services/test-result.service';
import { TestResultsApiDto } from 'src/app/models/test-result/test-results-api-dto';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-test-result-list',
    templateUrl: './test-result-list.component.html',
    styleUrls: ['./test-result-list.component.css']
})
export class TestResultListComponent implements OnInit {
    public columnsToDisplay: string[] = ['id', 'intervieweeName', 'passingStartTime', 'passingEndTime', 'score', 'read'];
    public testResultsApi: TestResultsApiDto = {} as TestResultsApiDto;
    public intervieweeNameFilter = '';
    public currentPageIndex = 0;
    public pageSize = 15;
    public pageSizeOptions: number[] = [this.pageSize, 10, 20];

    constructor(private testResultService: TestResultService, private titleService: Title) { }

    ngOnInit() {
        this.titleService.setTitle('Test results - QuizTest');

        this.setTestResultsPageWithFilter(this.currentPageIndex, this.pageSize);
    }

    public setTestResultsPageWithFilter(pageIndex: number, pageSize: number) {
        this.currentPageIndex = pageIndex;
        this.pageSize = pageSize;
        this.testResultService.getTestResults(this.intervieweeNameFilter, pageIndex, pageSize).subscribe(resultsApiResp => {
            this.testResultsApi = resultsApiResp.body;
        });
    }
}
