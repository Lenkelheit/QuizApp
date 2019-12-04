import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { UrlCreateComponent } from './components/url-create/url-create.component';
import { UrlListComponent } from './components/url-list/url-list.component';
import { UrlEditComponent } from './components/url-edit/url-edit.component';
import { UrlResultsComponent } from './components/url-results/url-results.component';

@NgModule({
    declarations: [UrlCreateComponent, UrlListComponent, UrlEditComponent, UrlResultsComponent],
    imports: [
        CommonModule,
        SharedModule
    ],
    exports: [
        UrlCreateComponent
    ]
})
export class UrlsModule { }
