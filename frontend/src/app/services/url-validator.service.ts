import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { UrlValidationResultDto } from '../models/url-validator/url-validation-result-dto';
import { IdentityUrlDto } from '../models/url-validator/identity-url-dto';
import { UserIdentificationResultDto } from '../models/url-validator/user-identification-result-dto';

@Injectable({
    providedIn: 'root'
})
export class UrlValidatorService {
    private routePrefix = '/api/url-validator';

    constructor(private httpService: HttpInternalService) { }

    public checkIsUrlValid(urlId: number) {
        return this.httpService.getRequest<UrlValidationResultDto>(`${this.routePrefix}/${urlId}`);
    }

    public identifyUser(urlDto: IdentityUrlDto) {
        return this.httpService.postRequest<UserIdentificationResultDto>(`${this.routePrefix}/identify-user`, urlDto);
    }
}
