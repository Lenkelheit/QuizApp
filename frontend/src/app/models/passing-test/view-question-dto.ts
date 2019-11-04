import { ViewQuestionOptionDto } from './view-question-option-dto';

export interface ViewQuestionDto {
    id: number;
    text: string;
    hint: string;
    timeLimitSeconds: string;

    testQuestionOptions: ViewQuestionOptionDto[];
}
