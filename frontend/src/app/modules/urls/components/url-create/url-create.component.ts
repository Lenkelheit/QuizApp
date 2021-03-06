import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NewUrlDto } from 'src/app/models/url/new-url-dto';
import { EndDateLessStartDateValidator } from '../../validators/end-date-less-start-date-validator';
import { ConfirmValidParentMatcher } from '../../error-state-matchers/confirm-valid-parent-matcher';
import { UrlService } from 'src/app/services/url.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { TestDto } from 'src/app/models/test/test-dto';
import { TestService } from 'src/app/services/test.service';
import { Error } from 'src/app/models/error/error';
import { ValidControlMatcher } from 'src/app/core/error-state-matchers/valid-control-matcher';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-url-create',
    templateUrl: './url-create.component.html',
    styleUrls: ['./url-create.component.css']
})
export class UrlCreateComponent implements OnInit {
    public newUrl: NewUrlDto = {} as NewUrlDto;
    public tests: TestDto[] = [];

    public errors: Error;

    public urlForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();
    public confirmValidParentMatcher = new ConfirmValidParentMatcher();

    constructor(private urlService: UrlService, private testService: TestService,
        // tslint:disable-next-line: align
        private formBuilder: FormBuilder, private router: Router, private titleService: Title) { }

    ngOnInit() {
        this.titleService.setTitle('Create url - QuizTest');

        this.urlForm = this.formBuilder.group({
            testId: ['', [Validators.required]],
            numberOfRuns: ['', [Validators.min(0)]],
            validFromTime: ['', [Validators.required]],
            validUntilTime: ['', [Validators.required]],
            intervieweeName: ['', [Validators.minLength(4), Validators.maxLength(32)]]
        }, {
            validators: EndDateLessStartDateValidator.validate
        });

        // Get all tests.
        this.testService.getTests(0, -1).subscribe(testsApiResp => this.tests = testsApiResp.body.tests);
    }

    get testId() {
        return this.urlForm.get('testId');
    }

    get numberOfRuns() {
        return this.urlForm.get('numberOfRuns');
    }

    get validFromTime() {
        return this.urlForm.get('validFromTime');
    }

    get validUntilTime() {
        return this.urlForm.get('validUntilTime');
    }

    get intervieweeName() {
        return this.urlForm.get('intervieweeName');
    }

    public sendNewUrl() {
        this.urlService.createUrl(this.newUrl).subscribe(createdUrlResp => {
            this.clearUrl();

            this.router.navigate(['/urls', createdUrlResp.body.id]);
        },
            (respErrors: HttpErrorResponse) => {
                this.errors = respErrors.error;
            }
        );
    }

    public clearUrl() {
        this.urlForm.reset();
        this.newUrl = {} as NewUrlDto;
        this.errors = null;
    }
}
