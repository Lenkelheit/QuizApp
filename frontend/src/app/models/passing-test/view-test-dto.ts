import { ViewQuestionDto } from './view-question-dto';

export interface ViewTestDto {
    id: number;
    title: string;
    description: string;
    timeLimitSeconds: string;
    lastModifiedDate: Date;
    authorId: number;

    testQuestions: ViewQuestionDto[];
}
