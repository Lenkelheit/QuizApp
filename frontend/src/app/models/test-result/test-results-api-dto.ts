import { TestResultDto } from './test-result-dto';

export interface TestResultsApiDto {
    totalCount: number;

    testResults: TestResultDto[];
}
