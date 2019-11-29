import { ViewQuestionOptionDto } from '../question-option/view-question-option-dto';

export interface ViewQuestionDto {
    id: number;
    text: string;
    hint: string;
    timeLimitSeconds: string;

    testQuestionOptions: ViewQuestionOptionDto[];
}
