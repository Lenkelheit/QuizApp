import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { QuestionCreateComponent } from './components/question-create/question-create.component';
import { QuestionOptionsModule } from '../question-options/question-options.module';

@NgModule({
    declarations: [QuestionCreateComponent],
    imports: [
        CommonModule,
        SharedModule,
        QuestionOptionsModule
    ],
    exports: [
        QuestionCreateComponent
    ]
})
export class QuestionsModule { }
