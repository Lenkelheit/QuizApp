import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { QuestionOptionService } from 'src/app/services/question-option.service';
import { NewQuestionOptionDto } from 'src/app/models/question-option/new-question-option-dto';
import { CreatedQuestionDto } from 'src/app/models/question/created-question-dto';
import { CreatedQuestionOptionDto } from 'src/app/models/question-option/created-question-option-dto';

@Component({
    selector: 'app-question-option-create',
    templateUrl: './question-option-create.component.html',
    styleUrls: ['./question-option-create.component.css']
})
export class QuestionOptionCreateComponent implements OnInit, OnDestroy {
    public newQuestionOptions: NewQuestionOptionDto[] = [{} as NewQuestionOptionDto];
    public createdQuestionOptions: CreatedQuestionOptionDto[] = [];

    @Input() deleteOptionsForms$: Observable<void>;
    @Input() sendOptionsAndDeleteForms$: Observable<CreatedQuestionDto>;
    @Output() deleteQuestion = new EventEmitter();
    private ngUnsubscribe = new Subject();

    questionOptionsForm: FormGroup;

    constructor(private questionOptionService: QuestionOptionService, private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.questionOptionsForm = this.formBuilder.group({
            questionOptions: this.formBuilder.array([
                this.formBuilder.group({
                    text: [''],
                    isRight: ['']
                })
            ])
        });

        this.sendOptionsAndDeleteForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(question => {
            this.newQuestionOptions.forEach(option => {
                option.questionId = question.id;

                if (typeof option.isRight === 'undefined') {
                    option.isRight = false;
                }

                this.sendOption(option);
            });

            this.clearOptions();

            this.deleteQuestion.emit();
        });

        this.deleteOptionsForms$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(() => {
            this.clearOptions();
        });
    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    get questionOptions() {
        return this.questionOptionsForm.get('questionOptions') as FormArray;
    }

    public addQuestionOption() {
        this.questionOptions.push(this.formBuilder.group({
            text: [''],
            isRight: ['']
        }));

        this.newQuestionOptions.push({} as NewQuestionOptionDto);
    }

    public deleteQuestionOption(index: number) {
        this.newQuestionOptions.splice(index, 1);
        this.questionOptions.removeAt(index);
    }

    public sendOption(option: NewQuestionOptionDto) {
        this.questionOptionService.createQuestionOption(option)
            .subscribe(respQuestionOption => this.createdQuestionOptions.push(respQuestionOption.body));
    }

    private clearOptions() {
        this.newQuestionOptions = [{} as NewQuestionOptionDto];
        this.createdQuestionOptions = [];
        this.questionOptions.clear();
        this.questionOptions.push(this.formBuilder.group({
            text: [''],
            isRight: ['']
        }));
    }
}
