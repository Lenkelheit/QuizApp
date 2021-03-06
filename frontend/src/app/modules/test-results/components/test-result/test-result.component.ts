import { Component, OnInit } from '@angular/core';
import { TestResultDetailDto } from 'src/app/models/test-result/test-result-detail-dto';
import { Subject } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { TestResultService } from 'src/app/services/test-result.service';
import { ResultAnswersApiDto } from 'src/app/models/result-answer/result-answers-api-dto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-test-result',
    templateUrl: './test-result.component.html',
    styleUrls: ['./test-result.component.css']
})
export class TestResultComponent implements OnInit {
    public testResult: TestResultDetailDto = {} as TestResultDetailDto;
    public resultAnswersApi: ResultAnswersApiDto = {} as ResultAnswersApiDto;
    public pageSize = 3;
    public pageSizeOptions: number[] = [this.pageSize, 5];

    constructor(private userService: UserService, private testResultService: TestResultService,
        // tslint:disable-next-line: align
        private authenticationService: AuthenticationService, private router: Router,
        // tslint:disable-next-line: align
        private route: ActivatedRoute, private titleService: Title) { }

    ngOnInit() {
        this.titleService.setTitle('Test result - QuizTest');

        this.authenticationService.getCurrentUser().subscribe(userLoggedinResp => {
            this.userService.currentUser = userLoggedinResp.body;
            if (!this.router.url.includes('passing-test') && !userLoggedinResp.body) {
                this.router.navigate(['/login']);
            }
        });

        const testResultId = parseInt(this.route.snapshot.paramMap.get('id'));

        this.testResultService.getTestResultById(testResultId).subscribe(testResultResp => {
            this.testResult = testResultResp.body;

            this.setResultAnswersPage(testResultId, 0, this.pageSize);
        });
    }

    get resultTimeTakenSeconds() {
        return Math.round((new Date(this.testResult.passingEndTime).getTime() / 1000) -
            (new Date(this.testResult.passingStartTime).getTime() / 1000));
    }

    get isTestChangedAfterTaken() {
        return new Date(this.testResult.test.lastModifiedDate).getTime() >
            new Date(this.testResult.passingEndTime).getTime();
    }

    public setResultAnswersPage(testResultId: number, pageIndex: number, pageSize: number) {
        this.testResultService.getAnswersByResultId(testResultId, pageIndex, pageSize)
            .subscribe(resultAnswersApiResp => {
                this.resultAnswersApi = resultAnswersApiResp.body;
            });
    }
}
