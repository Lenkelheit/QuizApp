import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators, FormControl } from '@angular/forms';
import { EndDateLessStartDateValidator } from '../../validators/end-date-less-start-date-validator';
import { ConfirmValidParentMatcher } from '../../error-state-matchers/confirm-valid-parent-matcher';
import { UrlService } from 'src/app/services/url.service';
import { Router, ActivatedRoute } from '@angular/router';
import { UpdateUrlDto } from 'src/app/models/url/update-url-dto';
import { HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Error } from 'src/app/models/error/error';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-url-edit',
    templateUrl: './url-edit.component.html',
    styleUrls: ['./url-edit.component.css']
})
export class UrlEditComponent implements OnInit {
    public updateUrl: UpdateUrlDto = {} as UpdateUrlDto;
    public baseUrl: string = environment.baseUrl;
    public errors: Error;

    public passUrlId: Subject<number> = new Subject<number>();

    public urlForm: FormGroup;

    public confirmValidParentMatcher = new ConfirmValidParentMatcher();

    constructor(private urlService: UrlService, private formBuilder: FormBuilder, private router: Router, private route: ActivatedRoute) { }

    ngOnInit() {
        this.urlForm = this.formBuilder.group({
            numberOfRuns: ['', [Validators.min(0)]],
            validFromTime: ['', [Validators.required]],
            validUntilTime: ['', [Validators.required]],
            intervieweeName: ['', [Validators.minLength(4), Validators.maxLength(32)]]
        }, {
            validators: EndDateLessStartDateValidator.validate
        });

        const urlId = parseInt(this.route.snapshot.paramMap.get('id'));

        this.urlService.getUrlById(urlId).subscribe(urlDetailResp => {
            this.updateUrl = urlDetailResp.body as UpdateUrlDto;

            this.passUrlId.next(urlId);
        });
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

    public sendUpdateUrl() {
        this.urlService.updateUrl(this.updateUrl).subscribe(() => {
            this.clearUrl();

            this.router.navigate(['/urls']);
        },
            (respErrors: HttpErrorResponse) => {
                this.errors = respErrors.error;
            }
        );
    }

    public clearUrl() {
        this.updateUrl = {
            id: this.updateUrl.id,
            test: this.updateUrl.test,
            testId: this.updateUrl.testId
        } as UpdateUrlDto;

        this.errors = null;
        this.urlForm.reset();
    }
}
