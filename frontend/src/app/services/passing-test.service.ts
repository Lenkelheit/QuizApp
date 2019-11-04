import { Injectable } from '@angular/core';
import { HttpInternalService } from '../http-internal.service';
import { IdentityUrlDto } from '../models/passing-test/identity-url-dto';
import { UrlValidationResultDto } from '../models/passing-test/url-validation-result-dto';
import { UserIdentificationResultDto } from '../models/passing-test/user-identification-result-dto';
import { ViewTestDto } from '../models/passing-test/view-test-dto';
import { UserUrlDto } from '../models/passing-test/user-url-dto';
import { CreatedTestResultDto } from '../models/passing-test/created-test-result-dto';

@Injectable({
    providedIn: 'root'
})
export class PassingTestService {

    public routePrefix = '/api/passing-test';

    constructor(private httpService: HttpInternalService) { }

    public checkIsUrlValid(urlId: number) {
        return this.httpService.getRequest<UrlValidationResultDto>(`${this.routePrefix}/${urlId}`);
    }

    public identifyUser(urlDto: IdentityUrlDto) {
        return this.httpService.postRequest<UserIdentificationResultDto>(`${this.routePrefix}/identify-user`, urlDto);
    }

    public getTestById(testId: number) {
        return this.httpService.getRequest<ViewTestDto>(`${this.routePrefix}/test/${testId}`);
    }

    public createTestResult(userUrlDto: UserUrlDto) {
        return this.httpService.postRequest<CreatedTestResultDto>(`${this.routePrefix}/test-result`, userUrlDto);
    }
}
