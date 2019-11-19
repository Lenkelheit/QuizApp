import { ResultQuestionOptionDto } from '../question-option/result-question-option-dto';

export interface ResultQuestionDto {
    id: number;
    text: string;
    hint: string;
    timeLimitSeconds: string;

    testQuestionOptions: ResultQuestionOptionDto[];
}
