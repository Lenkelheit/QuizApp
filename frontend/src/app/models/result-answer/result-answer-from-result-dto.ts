import { ResultAnswerOptionDetailDto } from '../result-answer-option/result-answer-option-detail-dto';
import { ResultQuestionDto } from '../question/result-question-dto';

export interface ResultAnswerFromResultDto {
    id: number;
    timeTakenSeconds: string;

    question: ResultQuestionDto;
    resultAnswerOptions: ResultAnswerOptionDetailDto[];
}
