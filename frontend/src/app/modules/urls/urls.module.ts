import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { UrlCreateComponent } from './components/url-create/url-create.component';

@NgModule({
    declarations: [UrlCreateComponent],
    imports: [
        CommonModule,
        SharedModule
    ],
    exports: [
        UrlCreateComponent
    ]
})
export class UrlsModule { }
