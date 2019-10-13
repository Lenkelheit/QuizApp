import { QuestionOptionDetailDto } from '../question-option/question-option-detail-dto';

export interface QuestionDetailDto {
    id: number;
    text: string;
    hint: string;
    timeLimitSeconds: string;

    testQuestionOptions: QuestionOptionDetailDto[];
}
