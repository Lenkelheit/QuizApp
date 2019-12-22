import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserIdentifyComponent } from './components/user-identify/user-identify.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    declarations: [UserIdentifyComponent],
    imports: [
        CommonModule,
        SharedModule
    ],
    exports: [UserIdentifyComponent]
})
export class UrlValidatorModule { }
