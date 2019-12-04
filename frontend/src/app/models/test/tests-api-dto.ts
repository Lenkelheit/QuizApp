import { TestDto } from './test-dto';

export interface TestsApiDto {
    totalCount: number;

    tests: TestDto[];
}
