import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { TestService } from 'src/app/services/test.service';
import { NewTestDto } from 'src/app/models/test/new-test-dto';
import { CreatedTestDto } from 'src/app/models/test/created-test-dto';
import { NewUrlDto } from 'src/app/models/url/new-url-dto';
import { NewQuestionDto } from 'src/app/models/question/new-question-dto';
import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormatTimeLimitValidator } from '../../../../shared/validators/format-time-limit-validator';
import { ValidationRegexes } from 'src/app/shared/validators/validation-regexes';
import { ValidControlMatcher } from 'src/app/shared/error-state-matchers/valid-control-matcher';

@Component({
    selector: 'app-test-create',
    templateUrl: './test-create.component.html',
    styleUrls: ['./test-create.component.css']
})
export class TestCreateComponent implements OnInit {
    public newTest: NewTestDto = {} as NewTestDto;

    public errors: Error;

    public urlsFormStatusInvalid = true;
    public questionsFormStatusInvalid = true;

    private deleteQuestionsForms: Subject<void> = new Subject<void>();
    private deleteUrlsForms: Subject<void> = new Subject<void>();
    private getQuestionsAndDeleteForms: Subject<void> = new Subject<void>();
    private getUrlsAndDeleteForms: Subject<void> = new Subject<void>();

    testForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    constructor(private testService: TestService, private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.testForm = this.formBuilder.group({
            title: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(64)]],
            description: ['', [Validators.minLength(4), Validators.maxLength(256)]],
            timeLimitSeconds: ['', [Validators.required, FormatTimeLimitValidator.validate(ValidationRegexes.timeLimitRegex)]]
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

    public sendTest() {
        this.newTest.authorId = 1;

        this.getQuestionsAndDeleteForms.next();
        this.getUrlsAndDeleteForms.next();

        this.testService.createTest(this.newTest).subscribe(() => { },
            (respErrors: HttpErrorResponse) => {
                this.errors = respErrors.error;
            }
        );

        this.clearTest();
    }

    public clearTestWithChildForms() {
        this.clearTest();
        this.deleteQuestionsForms.next();
        this.deleteUrlsForms.next();
    }

    private clearTest() {
        this.newTest = {} as NewTestDto;
        this.errors = null;
        this.testForm.reset();
    }

    private saveUrls(newUrls: NewUrlDto[]) {
        this.newTest.urls = newUrls;
    }

    private saveQuestions(newQuestions: NewQuestionDto[]) {
        this.newTest.testQuestions = newQuestions;
    }

    private setUrlsFormStatusInvalid(statusInvalid: boolean) {
        this.urlsFormStatusInvalid = statusInvalid;
    }

    private setQuestionsFormStatusInvalid(statusInvalid: boolean) {
        this.questionsFormStatusInvalid = statusInvalid;
    }

    public checkFormsStatus() {
        return this.testForm.invalid || this.urlsFormStatusInvalid || this.questionsFormStatusInvalid;
    }
}
