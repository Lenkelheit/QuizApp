import { CreatedResultAnswerDto } from './created-result-answer-dto';

export interface CreatedTestResultDto {
    id: number;
    intervieweeName: string;
    passingStartTime: Date;
    passingEndTime: Date;
    score: number;

    resultAnswers: CreatedResultAnswerDto[];
}
