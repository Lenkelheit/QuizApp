import { Component, OnInit } from '@angular/core';
import { TestService } from 'src/app/services/test.service';
import { NewTestDto } from 'src/app/models/test/new-test-dto';
import { HttpErrorResponse } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ValidationRegexes } from 'src/app/core/validators/validation-regexes';
import { ValidControlMatcher } from 'src/app/core/error-state-matchers/valid-control-matcher';
import { Router } from '@angular/router';
import { Error } from 'src/app/models/error/error';
import { FormatTimeLimitValidator } from 'src/app/core/validators/format-time-limit-validator';

@Component({
    selector: 'app-test-create',
    templateUrl: './test-create.component.html',
    styleUrls: ['./test-create.component.css']
})
export class TestCreateComponent implements OnInit {
    public newTest: NewTestDto = {} as NewTestDto;

    public errors: Error;

    public testForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();

    constructor(private testService: TestService, private formBuilder: FormBuilder, private router: Router) { }

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

    public sendNewTest() {
        this.newTest.authorId = 1;

        this.testService.createTest(this.newTest).subscribe(createdTestResp => {
            this.clearTest();

            this.router.navigate(['/tests', createdTestResp.body.id]);
        },
            (respErrors: HttpErrorResponse) => {
                this.errors = respErrors.error;
            }
        );
    }

    public clearTest() {
        this.newTest = {} as NewTestDto;
        this.errors = null;
        this.testForm.reset();
    }
}
