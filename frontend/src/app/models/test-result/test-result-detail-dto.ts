import { ResultAnswerDetailDto } from '../result-answer/result-answer-detail-dto';
import { ResultTestDto } from '../test/result-test-dto';

export interface TestResultDetailDto {
    id: number;
    intervieweeName: string;
    passingStartTime: Date;
    passingEndTime: Date;
    score: number;

    test: ResultTestDto;
}
