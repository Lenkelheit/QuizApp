import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { UrlService } from 'src/app/services/url.service';
import { NewUrlDto } from 'src/app/models/url/new-url-dto';
import { CreatedTestDto } from 'src/app/models/test/created-test-dto';
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
    @Input() sendUrlsAndDeleteForms$: Observable<CreatedTestDto>;

    private ngUnsubscribe = new Subject();

    urlsForm: FormGroup;

    constructor(private urlService: UrlService, private formBuilder: FormBuilder) { }

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

        this.sendUrlsAndDeleteForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe((test: CreatedTestDto) => {
            this.newUrls.forEach(url => {
                url.testId = test.id;

                this.sendUrl(url);
            });

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
    }

    public sendUrl(url: NewUrlDto) {
        this.urlService.createUrl(url).subscribe(respUrl => this.createdUrls.push(respUrl.body));
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
