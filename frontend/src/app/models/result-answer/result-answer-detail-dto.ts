import { ResultAnswerOptionDetailDto } from '../result-answer-option/result-answer-option-detail-dto';
import { ResultQuestionDto } from '../question/result-question-dto';

export interface ResultAnswerDetailDto {
    id: number;
    timeTakenSeconds: string;

    question: ResultQuestionDto;
    resultAnswerOptions: ResultAnswerOptionDetailDto[];
}
