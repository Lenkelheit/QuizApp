import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { TestService } from 'src/app/services/test.service';
import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ValidationRegexes } from 'src/app/core/validators/validation-regexes';
import { ValidControlMatcher } from 'src/app/core/error-state-matchers/valid-control-matcher';
import { Router, ActivatedRoute } from '@angular/router';
import { UpdateTestDto } from 'src/app/models/test/update-test-dto';
import { TestDetailDto } from 'src/app/models/test/test-detail-dto';
import { UpdateQuestionDto } from 'src/app/models/question/update-question-dto';
import { Error } from 'src/app/models/error/error';
import { FormatTimeLimitValidator } from 'src/app/core/validators/format-time-limit-validator';
import { UpdateQuestionOptionDto } from 'src/app/models/question-option/update-question-option-dto';

@Component({
    selector: 'app-test-edit',
    templateUrl: './test-edit.component.html',
    styleUrls: ['./test-edit.component.css']
})
export class TestEditComponent implements OnInit {
    private questionsFormStatusInvalid: boolean[] = [];

    public updateTest: UpdateTestDto = {} as UpdateTestDto;

    public errors: Error;

    public getQuestions: Subject<void> = new Subject<void>();
    public passTestId: Subject<number> = new Subject<number>();

    public testForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    constructor(private testService: TestService, private formBuilder: FormBuilder,
        // tslint:disable-next-line: align
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

            this.updateTest.testQuestions.forEach(() => {
                this.questionsFormStatusInvalid.push(false);
            });

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
            this.clearTest();

            this.router.navigate(['/tests']);
        },
            (respErrors: HttpErrorResponse) => {
                this.errors = respErrors.error;
            }
        );
    }

    public addQuestion() {
        this.updateTest.testQuestions.push({ testQuestionOptions: [{} as UpdateQuestionOptionDto] } as UpdateQuestionDto);
        this.questionsFormStatusInvalid.push(true);
    }

    public deleteQuestion(index: number) {
        this.updateTest.testQuestions.splice(index, 1);
        this.questionsFormStatusInvalid.splice(index, 1);
    }

    public clearTest() {
        this.testForm.reset();
        this.updateTest = {
            id: this.updateTest.id,
            authorId: this.updateTest.authorId,
            lastModifiedDate: this.updateTest.lastModifiedDate,
            testQuestions: []
        } as UpdateTestDto;

        this.questionsFormStatusInvalid = [];
        this.errors = null;
    }

    public checkFormsStatus() {
        return this.testForm.invalid || this.questionsFormStatusInvalid.some(value => value === true);
    }

    public setQuestion(index: number, updateQuestion: UpdateQuestionDto) {
        this.updateTest.testQuestions[index] = updateQuestion;
    }

    public setQuestionFormStatusInvalid(index: number, statusInvalid: boolean) {
        this.questionsFormStatusInvalid[index] = statusInvalid;
    }
}
