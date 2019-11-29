import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { TestResultDetailDto } from '../models/test-result/test-result-detail-dto';
import { ResultAnswersApiDto } from '../models/result-answer/result-answers-api-dto';
import { TestResultsApiDto } from '../models/test-result/test-results-api-dto';

@Injectable({
    providedIn: 'root'
})
export class TestResultService {
    private routePrefix = '/api/results';

    constructor(private httpService: HttpInternalService) { }

    public getTestResults(intervieweeNameFilter: string, page: number, amountResultsPerPage: number) {
        return this.httpService.getRequest<TestResultsApiDto>(`${this.routePrefix}`, {
            intervieweeNameFilter,
            page,
            amountResultsPerPage
        });
    }

    public getTestResultById(id: number) {
        return this.httpService.getRequest<TestResultDetailDto>(`${this.routePrefix}/${id}`);
    }

    public getAnswersByResultId(id: number, page: number, amountAnswersPerPage: number) {
        return this.httpService.getRequest<ResultAnswersApiDto>(`${this.routePrefix}/${id}/answers`, {
            page,
            amountAnswersPerPage
        });
    }
}
