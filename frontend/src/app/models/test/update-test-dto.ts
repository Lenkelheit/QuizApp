import { UpdateQuestionDto } from '../question/update-question-dto';

export interface UpdateTestDto {
    id: number;
    title: string;
    description: string;
    timeLimitSeconds: string;
    lastModifiedDate: Date;
    authorId: number;

    testQuestions: UpdateQuestionDto[];
}
