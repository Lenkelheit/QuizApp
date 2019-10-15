import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/http-internal.service';
import { TestDto } from '../models/test/test-dto';
import { NewTestDto } from '../models/test/new-test-dto';
import { CreatedTestDto } from '../models/test/created-test-dto';
import { UpdateTestDto } from '../models/test/update-test-dto';
import { UpdatedTestDto } from '../models/test/updated-test-dto';
import { TestDetailDto } from '../models/test/test-detail-dto';
import { UrlDto } from '../models/url/url-dto';

@Injectable({
    providedIn: 'root'
})
export class TestService {
    public routePrefix = '/api/tests';

    constructor(private httpService: HttpInternalService) { }

    public getTests() {
        return this.httpService.getRequest<TestDto[]>(`${this.routePrefix}`);
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

    public getUrlsByTestId(id: number) {
        return this.httpService.getRequest<UrlDto[]>(`${this.routePrefix}/${id}/urls`);
    }
}
