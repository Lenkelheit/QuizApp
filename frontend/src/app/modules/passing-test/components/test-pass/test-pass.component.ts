import { Component, OnInit } from '@angular/core';
import { PassingTestService } from 'src/app/services/passing-test.service';
import { IdentityUrlDto } from 'src/app/models/passing-test/identity-url-dto';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { UrlService } from 'src/app/services/url.service';
import { TestPreviewDto } from 'src/app/models/test/test-preview-dto';
import { Error } from 'src/app/models/error/error';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-test-pass',
    templateUrl: './test-pass.component.html',
    styleUrls: ['./test-pass.component.css']
})
export class TestPassComponent implements OnInit {

    private passTestId: Subject<number> = new Subject<number>();

    constructor() { }

    ngOnInit() {
    }

    public saveTestId(testId: number) {
        this.passTestId.next(testId);
    }
}
