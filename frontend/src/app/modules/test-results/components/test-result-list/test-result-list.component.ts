import { Component, OnInit } from '@angular/core';
import { TestResultService } from 'src/app/services/test-result.service';
import { TestResultDto } from 'src/app/models/test-result/test-result-dto';

@Component({
    selector: 'app-test-result-list',
    templateUrl: './test-result-list.component.html',
    styleUrls: ['./test-result-list.component.css']
})
export class TestResultListComponent implements OnInit {
    public columnsToDisplay: string[] = ['id', 'intervieweeName', 'passingStartTime', 'passingEndTime', 'score', 'read'];
    public testResults: TestResultDto[] = [];
    public intervieweeNameFilter = '';

    constructor(private testResultService: TestResultService) { }

    ngOnInit() {
        this.getTestResults(this.intervieweeNameFilter);
    }

    private getTestResults(intervieweeNameFilter: string) {
        this.testResultService.getTestResults(intervieweeNameFilter).subscribe(resultsResp => {
            this.testResults = resultsResp.body;
        });
    }
}
