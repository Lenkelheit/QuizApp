import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/http-internal.service';

@Injectable({
    providedIn: 'root'
})
export class QuestionOptionService {
    public routePrefix = '/api/question-options';

    constructor(private httpService: HttpInternalService) { }

}
