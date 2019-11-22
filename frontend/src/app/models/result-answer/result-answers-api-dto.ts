import { ResultAnswerFromResultDto } from './result-answer-from-result-dto';

export interface ResultAnswersApiDto {
    totalCount: number;

    resultAnswers: ResultAnswerFromResultDto[];
}
