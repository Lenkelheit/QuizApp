import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/http-internal.service';

@Injectable({
    providedIn: 'root'
})
export class QuestionService {
    public routePrefix = '/api/questions';

    constructor(private httpService: HttpInternalService) { }

}
