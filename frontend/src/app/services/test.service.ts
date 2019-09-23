import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/http-internal.service';
import { TestDto } from '../models/test/test-dto';
import { NewTestDto } from '../models/test/new-test-dto';
import { CreatedTestDto } from '../models/test/created-test-dto';

@Injectable({
    providedIn: 'root'
})
export class TestService {
    public routePrefix = '/api/tests';

    constructor(private httpService: HttpInternalService) { }

    public getTests() {
        return this.httpService.getRequest<TestDto[]>(`${this.routePrefix}`);
    }

    public createTest(test: NewTestDto) {
        return this.httpService.postRequest<CreatedTestDto>(`${this.routePrefix}`, test);
    }

    public deleteTest(id: number) {
        return this.httpService.deleteRequest<void>(`${this.routePrefix}/${id}`);
    }

}
