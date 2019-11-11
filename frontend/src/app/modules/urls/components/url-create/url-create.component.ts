import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators, FormControl } from '@angular/forms';
import { NewUrlDto } from 'src/app/models/url/new-url-dto';
import { EndDateLessStartDateValidator } from '../../validators/end-date-less-start-date-validator';
import { ConfirmValidParentMatcher } from '../../error-state-matchers/confirm-valid-parent-matcher';
import { UrlService } from 'src/app/services/url.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { TestDto } from 'src/app/models/test/test-dto';
import { TestService } from 'src/app/services/test.service';
import { ValidControlMatcher } from 'src/app/core/error-state-matchers/valid-control-matcher';

@Component({
    selector: 'app-url-create',
    templateUrl: './url-create.component.html',
    styleUrls: ['./url-create.component.css']
})
export class UrlCreateComponent implements OnInit {
    public newUrl: NewUrlDto = {} as NewUrlDto;
    public tests: TestDto[] = [];

    public errors: Error;

    public urlsForm: FormGroup;

    public validControlMatcher = new ValidControlMatcher();
    public confirmValidParentMatcher = new ConfirmValidParentMatcher();

    constructor(private urlService: UrlService, private testService: TestService,
                private formBuilder: FormBuilder, private router: Router) { }

    ngOnInit() {
        this.urlsForm = this.formBuilder.group({
            testId: ['', Validators.required],
            numberOfRuns: ['', [Validators.min(0)]],
            validFromTime: ['', [Validators.required]],
            validUntilTime: ['', [Validators.required]],
            intervieweeName: ['', [Validators.minLength(4), Validators.maxLength(32)]]
        }, {
            validators: EndDateLessStartDateValidator.validate
        });

        this.testService.getTests().subscribe(resp => this.tests = resp.body);
    }

    get testId() {
        return this.urlsForm.get('testId');
    }

    get numberOfRuns() {
        return this.urlsForm.get('numberOfRuns');
    }

    get validFromTime() {
        return this.urlsForm.get('validFromTime');
    }

    get validUntilTime() {
        return this.urlsForm.get('validUntilTime');
    }

    get intervieweeName() {
        return this.urlsForm.get('intervieweeName');
    }

    public sendNewUrls() {
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
        this.newUrl = {} as NewUrlDto;
        this.errors = null;
        this.urlsForm.reset();
    }
}
