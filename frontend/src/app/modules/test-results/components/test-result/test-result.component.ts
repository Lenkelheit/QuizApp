import { Component, OnInit } from '@angular/core';
import { TestResultDetailDto } from 'src/app/models/test-result/test-result-detail-dto';
import { Subject } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { TestResultService } from 'src/app/services/test-result.service';

@Component({
    selector: 'app-test-result',
    templateUrl: './test-result.component.html',
    styleUrls: ['./test-result.component.css']
})
export class TestResultComponent implements OnInit {
    public testResult: TestResultDetailDto = {} as TestResultDetailDto;
    public resultTimeTakenSeconds: number;
    public isTestChangedAfterTaken: boolean;

    constructor(private testResultService: TestResultService, private route: ActivatedRoute) { }

    ngOnInit() {
        const testResultId = parseInt(this.route.snapshot.paramMap.get('id'));

        this.testResultService.getTestResultById(testResultId).subscribe(testResultResp => {
            this.testResult = testResultResp.body;

            this.resultTimeTakenSeconds = Math.round((new Date(this.testResult.passingEndTime).getTime() / 1000) -
                (new Date(this.testResult.passingStartTime).getTime() / 1000));

            this.isTestChangedAfterTaken = new Date(this.testResult.test.lastModifiedDate).getTime() >
                new Date(this.testResult.passingEndTime).getTime();
        });
    }
}
