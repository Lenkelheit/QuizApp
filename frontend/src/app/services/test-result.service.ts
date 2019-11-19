import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { TestResultDetailDto } from '../models/test-result/test-result-detail-dto';

@Injectable({
    providedIn: 'root'
})
export class TestResultService {
    private routePrefix = '/api/results';

    constructor(private httpService: HttpInternalService) { }

    public getTestResultById(id: number) {
        return this.httpService.getRequest<TestResultDetailDto>(`${this.routePrefix}/${id}`);
    }
}
