import { Component, OnInit, OnDestroy, Input, Output, EventEmitter, ɵConsole } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NewQuestionDto } from 'src/app/models/question/new-question-dto';
import { CreatedQuestionDto } from 'src/app/models/question/created-question-dto';
import { NewQuestionOptionDto } from 'src/app/models/question-option/new-question-option-dto';

@Component({
    selector: 'app-question-create',
    templateUrl: './question-create.component.html',
    styleUrls: ['./question-create.component.css']
})
export class QuestionCreateComponent implements OnInit, OnDestroy {
    public newQuestions: NewQuestionDto[] = [{} as NewQuestionDto];
    public createdQuestions: CreatedQuestionDto[] = [];

    @Input() deleteQuestionsForms$: Observable<void>;
    @Input() getQuestionsAndDeleteForms$: Observable<void>;
    @Output() passUpNewQuestions: EventEmitter<NewQuestionDto[]> = new EventEmitter<NewQuestionDto[]>();

    private getOptionsAndDeleteForms: Subject<void>[] = [new Subject<void>()];
    private deleteOptionsForms: Subject<void> = new Subject<void>();
    private ngUnsubscribe = new Subject();

    public questionsForm: FormGroup;

    constructor(private formBuilder: FormBuilder) { }

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

        this.getQuestionsAndDeleteForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.getOptionsAndDeleteForms.forEach(elem => elem.next());

            this.passUpNewQuestions.emit(this.newQuestions);

            this.clearQuestions();
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

        this.getOptionsAndDeleteForms.push(new Subject<void>());
    }

    public deleteQuestion(index: number) {
        this.newQuestions.splice(index, 1);

        this.questions.removeAt(index);

        this.getOptionsAndDeleteForms.splice(index, 1);

        this.createdQuestions.splice(index, 1);

        if (this.newQuestions.length === 0) {
            this.clearQuestions();
        }
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
        this.getOptionsAndDeleteForms = [new Subject<void>()];
    }

    private clearQuestionsWithChildForms() {
        this.clearQuestions();
        this.deleteOptionsForms.next();
    }

    private saveQuestionOptions(index: number, newQuestionOptions: NewQuestionOptionDto[]) {
        this.newQuestions[index].testQuestionOptions = newQuestionOptions;
    }
}
