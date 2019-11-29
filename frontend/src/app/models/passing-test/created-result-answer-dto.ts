import { CreatedResultAnswerOptionDto } from './created-result-answer-option-dto';

export interface CreatedResultAnswerDto {
    id: number;
    timeTakenSeconds: string;

    resultAnswerOptions: CreatedResultAnswerOptionDto[];
}
