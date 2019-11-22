import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { TestResultDetailDto } from '../models/test-result/test-result-detail-dto';
import { TestResultDto } from '../models/test-result/test-result-dto';
import { ResultAnswersApiDto } from '../models/result-answer/result-answers-api-dto';

@Injectable({
    providedIn: 'root'
})
export class TestResultService {
    private routePrefix = '/api/results';

    constructor(private httpService: HttpInternalService) { }

    public getTestResults(intervieweeNameFilter: string) {
        return this.httpService.getRequest<TestResultDto[]>(`${this.routePrefix}`, {
            intervieweeNameFilter
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
