import { TestDto } from '../test/test-dto';

export interface UrlDetailDto {
    id: number;
    numberOfRuns: number;
    validFromTime: Date;
    validUntilTime: Date;
    intervieweeName: string;
    testId: number;
    test: TestDto;
}
