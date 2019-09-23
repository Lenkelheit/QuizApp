import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { QuestionService } from 'src/app/services/question.service';
import { NewQuestionDto } from 'src/app/models/question/new-question-dto';
import { CreatedTestDto } from 'src/app/models/test/created-test-dto';
import { CreatedQuestionDto } from 'src/app/models/question/created-question-dto';

@Component({
    selector: 'app-question-create',
    templateUrl: './question-create.component.html',
    styleUrls: ['./question-create.component.css']
})
export class QuestionCreateComponent implements OnInit, OnDestroy {
    public newQuestions: NewQuestionDto[] = [{} as NewQuestionDto];
    public createdQuestions: CreatedQuestionDto[] = [];

    @Input() deleteQuestionsForms$: Observable<void>;
    @Input() sendQuestions$: Observable<CreatedTestDto>;
    private sendOptionsAndDeleteForms: Subject<CreatedQuestionDto>[] = [new Subject<CreatedQuestionDto>()];
    private deleteOptionsForms: Subject<void> = new Subject<void>();
    private ngUnsubscribe = new Subject();

    public questionsForm: FormGroup;

    constructor(private questionService: QuestionService, private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.questionsForm = this.formBuilder.group({
            questions: this.formBuilder.array([
                this.formBuilder.group({
                    text: [''],
                    hint: [''],
                    timeLimitSeconds: ['']
                })
            ])
        });

        this.sendQuestions$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(test => {
            this.newQuestions.forEach((question, index) => {
                question.testId = test.id;

                this.sendQuestion(question);
            });
        });

        this.deleteQuestionsForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.clearQuestionsWithChildForms();
        });
    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    get questions() {
        return this.questionsForm.get('questions') as FormArray;
    }

    public addQuestion() {
        this.questions.push(this.formBuilder.group({
            text: [''],
            hint: [''],
            timeLimitSeconds: ['']
        }));
        this.newQuestions.push({} as NewQuestionDto);

        this.sendOptionsAndDeleteForms.push(new Subject<CreatedQuestionDto>());
    }

    public deleteQuestion(index: number) {
        this.newQuestions.splice(index, 1);

        this.questions.removeAt(index);

        this.sendOptionsAndDeleteForms.splice(index, 1);

        this.createdQuestions.splice(index, 1);

        if (this.newQuestions.length === 0) {
            this.clearQuestions();
        }
    }

    public sendQuestion(question: NewQuestionDto) {
        const timeLimitSeconds: Date = new Date(0, 0, 0, 0, 0, 0);
        timeLimitSeconds.setSeconds(parseInt(question.timeLimitSeconds));
        question.timeLimitSeconds = timeLimitSeconds.toLocaleTimeString();

        this.questionService.createQuestion(question).subscribe(respQuestion => {
            this.createdQuestions.push(respQuestion.body);
            this.sendOptionsAndDeleteForms[this.createdQuestions.length - 1].next(respQuestion.body);
        });
    }

    private clearQuestions() {
        this.newQuestions = [{} as NewQuestionDto];
        this.createdQuestions = [];
        this.questions.clear();
        this.questions.push(this.formBuilder.group({
            text: [''],
            hint: [''],
            timeLimitSeconds: ['']
        }));
        this.sendOptionsAndDeleteForms = [new Subject<CreatedQuestionDto>()];
    }

    private clearQuestionsWithChildForms() {
        this.clearQuestions();
        this.deleteOptionsForms.next();
    }
}
