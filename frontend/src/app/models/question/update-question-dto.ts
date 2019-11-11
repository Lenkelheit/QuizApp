import { UpdateQuestionOptionDto } from '../question-option/update-question-option-dto';

export interface UpdateQuestionDto {
    id: number;
    text: string;
    hint: string;
    timeLimitSeconds: string;

    testQuestionOptions: UpdateQuestionOptionDto[];
}
