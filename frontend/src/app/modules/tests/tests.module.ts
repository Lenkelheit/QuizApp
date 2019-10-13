import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestListComponent } from './components/test-list/test-list.component';
import { SharedModule } from '../../shared/shared.module';
import { TestCreateComponent } from './components/test-create/test-create.component';
import { QuestionsModule } from '../questions/questions.module';
import { UrlsModule } from '../urls/urls.module';
import { TestEditComponent } from './components/test-edit/test-edit.component';

@NgModule({
    declarations: [TestListComponent, TestCreateComponent, TestEditComponent],
    imports: [
        CommonModule,
        SharedModule,
        QuestionsModule,
        UrlsModule
    ],
    exports: [
        TestListComponent
    ]
})
export class TestsModule { }
