import { Component, OnInit } from '@angular/core';
import { IdentityUrlDto } from 'src/app/models/url-validator/identity-url-dto';
import { Subject, BehaviorSubject } from 'rxjs';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-test-pass',
    templateUrl: './test-pass.component.html',
    styleUrls: ['./test-pass.component.css']
})
export class TestPassComponent implements OnInit {
    public isUserIdentified = false;
    public istestSent = false;
    public testResultId: number;

    public passIdentityUrl: BehaviorSubject<IdentityUrlDto>;

    constructor(private titleService: Title) { }

    ngOnInit() {
        this.titleService.setTitle('Test form - QuizTest');
    }

    public setIdentityUrl(identityUrl: IdentityUrlDto) {
        this.passIdentityUrl = new BehaviorSubject<IdentityUrlDto>(identityUrl);

        this.isUserIdentified = true;
    }

    public setTestResultId(testResultId: number) {
        this.testResultId = testResultId;
        this.istestSent = true;
    }
}
