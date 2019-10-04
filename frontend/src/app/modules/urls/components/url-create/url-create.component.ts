import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators, FormControl } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NewUrlDto } from 'src/app/models/url/new-url-dto';
import { CreatedUrlDto } from 'src/app/models/url/created-url-dto';
import { EndDateLessStartDateValidator } from '../../validators/end-date-less-start-date-validator';
import { ConfirmValidParentMatcher } from '../error-state-matchers/confirm-valid-parent-matcher';

@Component({
    selector: 'app-url-create',
    templateUrl: './url-create.component.html',
    styleUrls: ['./url-create.component.css']
})
export class UrlCreateComponent implements OnInit, OnDestroy {
    public newUrls: NewUrlDto[] = [{} as NewUrlDto];
    public createdUrls: CreatedUrlDto[] = [];

    @Input() deleteUrlsForms$: Observable<void>;
    @Input() getUrlsAndDeleteForms$: Observable<void>;
    @Output() passUpNewUrls: EventEmitter<NewUrlDto[]> = new EventEmitter<NewUrlDto[]>();
    @Output() passUpUrlsFormStatusInvalid: EventEmitter<boolean> = new EventEmitter<boolean>();
    private ngUnsubscribe = new Subject();

    urlsForm: FormGroup;

    public confirmValidParentMatcher = new ConfirmValidParentMatcher();

    constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.urlsForm = this.formBuilder.group({
            urls: this.formBuilder.array([
                this.addUrlFormGroup()
            ])
        });

        this.urlsForm.statusChanges.subscribe(status => {
            this.passUpUrlsFormStatusInvalid.emit(status === 'INVALID');
        });

        this.getUrlsAndDeleteForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.passUpNewUrls.emit(this.newUrls);

            this.clearUrls();
        });

        this.deleteUrlsForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.clearUrls();
        });
    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    get urls() {
        return this.urlsForm.get('urls') as FormArray;
    }

    public getUrl(index: number) {
        return this.urls.controls[index] as FormGroup;
    }

    public getNumberOfRuns(index: number) {
        return this.getUrl(index).controls.numberOfRuns;
    }

    public getValidFromTime(index: number) {
        return this.getUrl(index).controls.validFromTime;
    }

    public getValidUntilTime(index: number) {
        return this.getUrl(index).controls.validUntilTime;
    }

    public getIntervieweeName(index: number) {
        return this.getUrl(index).controls.intervieweeName;
    }

    public addUrl() {
        this.urls.push(this.addUrlFormGroup());

        this.newUrls.push({} as NewUrlDto);
    }

    public addUrlFormGroup() {
        return this.formBuilder.group({
            numberOfRuns: ['', [Validators.min(0)]],
            validFromTime: ['', [Validators.required]],
            validUntilTime: ['', [Validators.required]],
            intervieweeName: ['', [Validators.minLength(4), Validators.maxLength(32)]]
        }, {
            validators: EndDateLessStartDateValidator.validate
        });
    }

    public deleteUrl(index: number) {
        this.newUrls.splice(index, 1);
        this.urls.removeAt(index);

        if (this.newUrls.length === 0) {
            this.clearUrls();
        }
    }

    private clearUrls() {
        this.newUrls = [{} as NewUrlDto];
        this.createdUrls = [];
        this.urls.clear();
        this.urls.push(this.addUrlFormGroup());
    }
}
