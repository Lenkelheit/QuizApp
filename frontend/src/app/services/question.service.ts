import { Injectable } from '@angular/core';
import { HttpInternalService } from 'src/app/http-internal.service';
import { NewQuestionDto } from '../models/question/new-question-dto';
import { CreatedQuestionDto } from '../models/question/created-question-dto';

@Injectable({
    providedIn: 'root'
})
export class QuestionService {
    public routePrefix = '/api/questions';

    constructor(private httpService: HttpInternalService) { }

    public createQuestion(question: NewQuestionDto) {
        return this.httpService.postRequest<CreatedQuestionDto>(`${this.routePrefix}`, question);
    }
}
