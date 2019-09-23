import { NewQuestionDto } from '../question/new-question-dto';

export interface NewTestDto {
    title: string;
    description: string;
    timeLimitSeconds: string;
    authorId: number;
}
