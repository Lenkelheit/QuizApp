import { Component } from '@angular/core';
import { IdentityUrlDto } from 'src/app/models/passing-test/identity-url-dto';
import { Subject, BehaviorSubject } from 'rxjs';

@Component({
    selector: 'app-test-pass',
    templateUrl: './test-pass.component.html',
    styleUrls: ['./test-pass.component.css']
})
export class TestPassComponent {
    public isUserIdentified = false;

    public passIdentityUrl: BehaviorSubject<IdentityUrlDto>;

    public setIdentityUrl(identityUrl: IdentityUrlDto) {
        this.passIdentityUrl = new BehaviorSubject<IdentityUrlDto>(identityUrl);

        this.isUserIdentified = true;
    }
}
