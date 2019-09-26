import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/http-internal.service';

@Injectable({
    providedIn: 'root'
})
export class UrlService {
    public routePrefix = '/api/urls';

    constructor(private httpService: HttpInternalService) { }

}
