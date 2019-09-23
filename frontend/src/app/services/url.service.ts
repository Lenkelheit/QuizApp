import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/http-internal.service';
import { NewUrlDto } from '../models/url/new-url-dto';
import { CreatedUrlDto } from '../models/url/created-url-dto';

@Injectable({
    providedIn: 'root'
})
export class UrlService {
    public routePrefix = '/api/urls';

    constructor(private httpService: HttpInternalService) { }

    public createUrl(url: NewUrlDto) {
        return this.httpService.postRequest<CreatedUrlDto>(`${this.routePrefix}`, url);
    }
}
