import { NewQuestionDto } from '../question/new-question-dto';
import { NewUrlDto } from '../url/new-url-dto';

export interface NewTestDto {
    title: string;
    description: string;
    timeLimitSeconds: string;
    authorId: number;

    urls: NewUrlDto[];
    testQuestions: NewQuestionDto[];
}
