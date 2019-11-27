import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { UserUrlDto } from '../models/passing-test/user-url-dto';
import { CreatedTestResultDto } from '../models/passing-test/created-test-result-dto';

@Injectable({
    providedIn: 'root'
})
export class PassingTestService {
    private routePrefix = '/api/passing-test';

    constructor(private httpService: HttpInternalService) { }

    public createTestResult(userUrlDto: UserUrlDto) {
        return this.httpService.postRequest<CreatedTestResultDto>(`${this.routePrefix}/test-result`, userUrlDto);
    }
}
