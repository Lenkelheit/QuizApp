import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { QuestionCreateEditComponent } from './components/question-create-edit/question-create-edit.component';
import { QuestionOptionsModule } from '../question-options/question-options.module';

@NgModule({
    declarations: [QuestionCreateEditComponent],
    imports: [
        CommonModule,
        SharedModule,
        QuestionOptionsModule
    ],
    exports: [
        QuestionCreateEditComponent
    ]
})
export class QuestionsModule { }
