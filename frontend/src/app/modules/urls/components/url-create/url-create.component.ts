import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators, FormControl } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NewUrlDto } from 'src/app/models/url/new-url-dto';
import { CreatedUrlDto } from 'src/app/models/url/created-url-dto';

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
    private ngUnsubscribe = new Subject();

    urlsForm: FormGroup;

    constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.urlsForm = this.formBuilder.group({
            urls: this.formBuilder.array([
                this.formBuilder.group({
                    numberOfRuns: [''],
                    validFromTime: [''],
                    validUntilTime: [''],
                    intervieweeName: ['']
                })
            ])
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

    public addUrl() {
        this.urls.push(this.formBuilder.group({
            numberOfRuns: [''],
            validFromTime: [''],
            validUntilTime: [''],
            intervieweeName: ['']
        }));

        this.newUrls.push({} as NewUrlDto);
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
        this.urls.push(this.formBuilder.group({
            numberOfRuns: [''],
            validFromTime: [''],
            validUntilTime: [''],
            intervieweeName: ['']
        }));
    }
}
