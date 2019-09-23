import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/http-internal.service';
import { NewQuestionOptionDto } from '../models/question-option/new-question-option-dto';
import { CreatedQuestionOptionDto } from '../models/question-option/created-question-option-dto';

@Injectable({
    providedIn: 'root'
})
export class QuestionOptionService {
    public routePrefix = '/api/question-options';

    constructor(private httpService: HttpInternalService) { }

    public createQuestionOption(questionOption: NewQuestionOptionDto) {
        return this.httpService.postRequest<CreatedQuestionOptionDto>(`${this.routePrefix}`, questionOption);
    }
}
