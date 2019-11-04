import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TestPassComponent } from './components/test-pass/test-pass.component';
import { UserIdentifyComponent } from './components/user-identify/user-identify.component';
import { TestFormComponent } from './components/test-form/test-form.component';
import { QuestionsFormComponent } from './components/questions-form/questions-form.component';
import { QuestionOptionsFormComponent } from './components/question-options-form/question-options-form.component';
import { CounterDirective } from './directives/counter.directive';
import { TimeSecondsToHhMmSsPipe } from './pipes/time-seconds-to-hh-mm-ss.pipe';

@NgModule({
    declarations: [TestPassComponent, UserIdentifyComponent, TestFormComponent,
        QuestionsFormComponent, QuestionOptionsFormComponent, CounterDirective, TimeSecondsToHhMmSsPipe],
    imports: [
        CommonModule,
        SharedModule
    ]
})
export class PassingTestModule { }
