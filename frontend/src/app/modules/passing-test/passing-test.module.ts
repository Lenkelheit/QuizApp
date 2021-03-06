import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TestPassComponent } from './components/test-pass/test-pass.component';
import { TestFormComponent } from './components/test-form/test-form.component';
import { QuestionFormComponent } from './components/question-form/question-form.component';
import { QuestionOptionFormComponent } from './components/question-option-form/question-option-form.component';
import { CounterDirective } from './directives/counter.directive';
import { UrlValidatorModule } from '../url-validator/url-validator.module';

@NgModule({
    declarations: [TestPassComponent, TestFormComponent,
        QuestionFormComponent, QuestionOptionFormComponent, CounterDirective],
    imports: [
        CommonModule,
        SharedModule,
        UrlValidatorModule
    ]
})
export class PassingTestModule { }
