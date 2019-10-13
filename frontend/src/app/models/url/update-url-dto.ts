import { TestDto } from '../test/test-dto';

export interface UpdateUrlDto {
    id: number;
    numberOfRuns: number;
    validFromTime: Date;
    validUntilTime: Date;
    intervieweeName: string;
    testId: number;
    test: TestDto;
}
