import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { QuestionOptionCreateComponent } from './components/question-option-create/question-option-create.component';

@NgModule({
    declarations: [QuestionOptionCreateComponent],
    imports: [
        CommonModule,
        SharedModule
    ],
    exports: [
        QuestionOptionCreateComponent
    ]
})
export class QuestionOptionsModule { }
