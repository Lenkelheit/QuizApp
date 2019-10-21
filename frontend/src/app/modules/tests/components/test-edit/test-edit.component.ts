import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { TestService } from 'src/app/services/test.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ValidationRegexes } from 'src/app/shared/validators/validation-regexes';
import { ValidControlMatcher } from 'src/app/shared/error-state-matchers/valid-control-matcher';
import { Router, ActivatedRoute } from '@angular/router';
import { UpdateTestDto } from 'src/app/models/test/update-test-dto';
import { TestDetailDto } from 'src/app/models/test/test-detail-dto';
import { UpdateQuestionDto } from 'src/app/models/question/update-question-dto';
import { FormatTimeLimitValidator } from 'src/app/shared/validators/format-time-limit-validator';
import { Error } from 'src/app/models/error/error';

@Component({
    selector: 'app-test-edit',
    templateUrl: './test-edit.component.html',
    styleUrls: ['./test-edit.component.css']
})
export class TestEditComponent implements OnInit {
    public updateTest: UpdateTestDto = {} as UpdateTestDto;

    public errors: Error;

    public questionsFormStatusInvalid: boolean;

    private deleteQuestionsForms: Subject<void> = new Subject<void>();
    private getQuestions: Subject<void> = new Subject<void>();
    private initializeQuestions: Subject<UpdateQuestionDto[]> = new Subject<UpdateQuestionDto[]>();
    private passTestId: Subject<number> = new Subject<number>();

    testForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    constructor(private testService: TestService, private formBuilder: FormBuilder,
                private router: Router, private route: ActivatedRoute) { }

    ngOnInit() {
        this.testForm = this.formBuilder.group({
            title: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]],
            description: ['', [Validators.minLength(4), Validators.maxLength(256)]],
            timeLimitSeconds: ['', [Validators.required, FormatTimeLimitValidator.validate(ValidationRegexes.timeLimitRegex)]]
        });

        const testId = parseInt(this.route.snapshot.paramMap.get('id'));

        this.testService.getTestById(testId).subscribe(testDetailResp => {
            this.updateTest = testDetailResp.body as UpdateTestDto;

            this.initializeQuestions.next(this.updateTest.testQuestions);

            this.questionsFormStatusInvalid = false;

            this.passTestId.next(testId);
        });
    }

    get title() {
        return this.testForm.get('title');
    }

    get description() {
        return this.testForm.get('description');
    }

    get timeLimitSeconds() {
        return this.testForm.get('timeLimitSeconds');
    }

    public sendUpdateTest() {
        this.getQuestions.next();

        this.testService.updateTest(this.updateTest).subscribe(() => {
            this.clearTestWithChildForms();

            this.router.navigate(['/tests']);
        },
            (respErrors: HttpErrorResponse) => {
                this.errors = respErrors.error;
            }
        );
    }

    public clearTestWithChildForms() {
        this.clearTest();
        this.deleteQuestionsForms.next();
    }

    private clearTest() {
        const updateTestId = this.updateTest.id;
        const updateTestAuthorId = this.updateTest.authorId;
        const updateTestLastModifiedDate = this.updateTest.lastModifiedDate;

        this.updateTest = {} as UpdateTestDto;
        this.updateTest.id = updateTestId;
        this.updateTest.authorId = updateTestAuthorId;
        this.updateTest.lastModifiedDate = updateTestLastModifiedDate;

        this.errors = null;
        this.testForm.reset();
    }

    private saveQuestions(updateQuestions: UpdateQuestionDto[]) {
        this.updateTest.testQuestions = updateQuestions;
    }

    private setQuestionsFormStatusInvalid(statusInvalid: boolean) {
        this.questionsFormStatusInvalid = statusInvalid;
    }

    public checkFormsStatus() {
        return this.testForm.invalid || this.questionsFormStatusInvalid;
    }
}
