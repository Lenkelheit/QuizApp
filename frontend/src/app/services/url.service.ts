import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/http-internal.service';
import { UrlDto } from '../models/url/url-dto';
import { NewUrlDto } from '../models/url/new-url-dto';
import { CreatedUrlDto } from '../models/url/created-url-dto';
import { UpdateUrlDto } from '../models/url/update-url-dto';
import { UpdatedUrlDto } from '../models/url/updated-url-dto';
import { UrlDetailDto } from '../models/url/url-detail-dto';

@Injectable({
    providedIn: 'root'
})
export class UrlService {
    public routePrefix = '/api/urls';

    constructor(private httpService: HttpInternalService) { }

    public getUrls() {
        return this.httpService.getRequest<UrlDto[]>(`${this.routePrefix}`);
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
}
