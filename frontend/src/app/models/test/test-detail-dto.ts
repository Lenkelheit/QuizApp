import { QuestionDetailDto } from '../question/question-detail-dto';

export interface TestDetailDto {
    id: number;
    title: string;
    description: string;
    timeLimitSeconds: string;
    lastModifiedDate: Date;
    authorId: number;

    testQuestions: QuestionDetailDto[];
}
