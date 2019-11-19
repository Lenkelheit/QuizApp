import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestResultComponent } from './components/test-result/test-result.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { TestComponent } from './components/test/test.component';
import { QuestionAnswerComponent } from './components/question-answer/question-answer.component';
import { QuestionOptionComponent } from './components/question-option/question-option.component';

@NgModule({
    declarations: [TestResultComponent, TestComponent, QuestionAnswerComponent, QuestionOptionComponent],
    imports: [
        CommonModule,
        SharedModule
    ]
})
export class TestResultsModule { }
