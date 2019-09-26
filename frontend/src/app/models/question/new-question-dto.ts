import { NewQuestionOptionDto } from '../question-option/new-question-option-dto';

export interface NewQuestionDto {
    text: string;
    hint: string;
    timeLimitSeconds: string;

    testQuestionOptions: NewQuestionOptionDto[];
}
