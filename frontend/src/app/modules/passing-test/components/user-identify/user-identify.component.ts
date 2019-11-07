import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { PassingTestService } from 'src/app/services/passing-test.service';
import { IdentityUrlDto } from 'src/app/models/passing-test/identity-url-dto';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { UrlService } from 'src/app/services/url.service';
import { TestPreviewDto } from 'src/app/models/test/test-preview-dto';
import { Error } from 'src/app/models/error/error';
import { UrlValidationResultDto } from 'src/app/models/passing-test/url-validation-result-dto';

@Component({
    selector: 'app-user-identify',
    templateUrl: './user-identify.component.html',
    styleUrls: ['./user-identify.component.css']
})
export class UserIdentifyComponent implements OnInit {
    public identityUrl: IdentityUrlDto = {} as IdentityUrlDto;
    public testPreview: TestPreviewDto = {} as TestPreviewDto;
    public urlValidationResult: UrlValidationResultDto = {} as UrlValidationResultDto;

    public errors: Error = {} as Error;

    @Output() passUpIdentityUrl: EventEmitter<IdentityUrlDto> = new EventEmitter<IdentityUrlDto>();

    public urlForm: FormGroup;

    constructor(private passingTestService: PassingTestService, private urlService: UrlService, private formBuilder: FormBuilder,
        // tslint:disable-next-line: align
        private router: Router, private route: ActivatedRoute) { }

    ngOnInit() {
        const urlId = parseInt(this.route.snapshot.paramMap.get('id'));

        this.passingTestService.checkIsUrlValid(urlId).subscribe(urlValidationResultResp => {
            this.urlValidationResult = urlValidationResultResp.body;

            if (this.urlValidationResult.isValid) {
                this.urlForm = this.formBuilder.group({
                    intervieweeName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(32)]]
                });

                this.urlService.getTestByUrlId(urlId).subscribe(testResp => {
                    this.testPreview = testResp.body;

                    this.identityUrl.testId = this.testPreview.id;
                });

                this.identityUrl.id = urlId;
            }
        });
    }

    get intervieweeName() {
        return this.urlForm.get('intervieweeName');
    }

    public sendUrlOnValidation() {
        this.passingTestService.identifyUser(this.identityUrl).subscribe(userIdentificationResultResp => {
            const userIdentificationResult = userIdentificationResultResp.body;

            if (userIdentificationResult.isUrlValid) {
                if (userIdentificationResult.isIdentified) {

                    this.passUpIdentityUrl.emit(this.identityUrl);

                } else {
                    this.errors.errors = userIdentificationResult.errors;
                }
            } else {
                this.urlValidationResult.isValid = userIdentificationResult.isUrlValid;
                this.urlValidationResult.errors = userIdentificationResult.errors;
            }
        },
            (respErrors: HttpErrorResponse) => {
                this.errors = respErrors.error;
            }
        );
    }
}
