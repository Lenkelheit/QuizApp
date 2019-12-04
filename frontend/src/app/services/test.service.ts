import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/services/http-internal.service';
import { NewTestDto } from '../models/test/new-test-dto';
import { CreatedTestDto } from '../models/test/created-test-dto';
import { UpdateTestDto } from '../models/test/update-test-dto';
import { UpdatedTestDto } from '../models/test/updated-test-dto';
import { TestDetailDto } from '../models/test/test-detail-dto';
import { ViewTestDto } from '../models/test/view-test-dto';
import { TestsApiDto } from '../models/test/tests-api-dto';
import { UrlsApiDto } from '../models/url/urls-api-dto';
import { TestResultsApiDto } from '../models/test-result/test-results-api-dto';

@Injectable({
    providedIn: 'root'
})
export class TestService {
    private routePrefix = '/api/tests';

    constructor(private httpService: HttpInternalService) { }

    public getTests(page: number, amountTestsPerPage: number) {
        return this.httpService.getRequest<TestsApiDto>(`${this.routePrefix}`, {
            page,
            amountTestsPerPage
        });
    }

    public getTestById(id: number) {
        return this.httpService.getRequest<TestDetailDto>(`${this.routePrefix}/${id}`);
    }

    public createTest(test: NewTestDto) {
        return this.httpService.postRequest<CreatedTestDto>(`${this.routePrefix}`, test);
    }

    public updateTest(test: UpdateTestDto) {
        return this.httpService.putRequest<UpdatedTestDto>(`${this.routePrefix}`, test);
    }

    public deleteTest(id: number) {
        return this.httpService.deleteRequest<void>(`${this.routePrefix}/${id}`);
    }

    public getUrlsByTestId(id: number, page: number, amountUrlsPerPage: number) {
        return this.httpService.getRequest<UrlsApiDto>(`${this.routePrefix}/${id}/urls`, {
            page,
            amountUrlsPerPage
        });
    }

    public getResultsByTestId(id: number, page: number, amountResultsPerPage: number) {
        return this.httpService.getRequest<TestResultsApiDto>(`${this.routePrefix}/${id}/results`, {
            page,
            amountResultsPerPage
        });
    }

    public getPassingTestById(testId: number) {
        return this.httpService.getRequest<ViewTestDto>(`${this.routePrefix}/passing-test/${testId}`);
    }
}
