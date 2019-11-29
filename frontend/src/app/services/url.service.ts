import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/services/http-internal.service';
import { NewUrlDto } from '../models/url/new-url-dto';
import { CreatedUrlDto } from '../models/url/created-url-dto';
import { UpdateUrlDto } from '../models/url/update-url-dto';
import { UpdatedUrlDto } from '../models/url/updated-url-dto';
import { UrlDetailDto } from '../models/url/url-detail-dto';
import { TestPreviewDto } from '../models/test/test-preview-dto';
import { UrlsApiDto } from '../models/url/urls-api-dto';
import { TestResultsApiDto } from '../models/test-result/test-results-api-dto';

@Injectable({
    providedIn: 'root'
})
export class UrlService {
    private routePrefix = '/api/urls';

    constructor(private httpService: HttpInternalService) { }

    public getUrls(page: number, amountUrlsPerPage: number) {
        return this.httpService.getRequest<UrlsApiDto>(`${this.routePrefix}`, {
            page,
            amountUrlsPerPage
        });
    }

    public getUrlById(id: number) {
        return this.httpService.getRequest<UrlDetailDto>(`${this.routePrefix}/${id}`);
    }

    public createUrl(url: NewUrlDto) {
        return this.httpService.postRequest<CreatedUrlDto>(`${this.routePrefix}`, url);
    }

    public updateUrl(url: UpdateUrlDto) {
        return this.httpService.putRequest<UpdatedUrlDto>(`${this.routePrefix}`, url);
    }

    public getTestByUrlId(urlId: number) {
        return this.httpService.getRequest<TestPreviewDto>(`${this.routePrefix}/${urlId}/test`);
    }

    public getTestResultsByUrlId(id: number, page: number, amountResultsPerPage: number) {
        return this.httpService.getRequest<TestResultsApiDto>(`${this.routePrefix}/${id}/results`, {
            page,
            amountResultsPerPage
        });
    }
}
