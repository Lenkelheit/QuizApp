import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { QuestionOptionCreateEditComponent } from './components/question-option-create-edit/question-option-create-edit.component';

@NgModule({
    declarations: [QuestionOptionCreateEditComponent],
    imports: [
        CommonModule,
        SharedModule
    ],
    exports: [
        QuestionOptionCreateEditComponent
    ]
})
export class QuestionOptionsModule { }
