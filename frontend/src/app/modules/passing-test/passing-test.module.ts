import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TestPassComponent } from './components/test-pass/test-pass.component';
import { UserIdentifyComponent } from './components/user-identify/user-identify.component';

@NgModule({
    declarations: [TestPassComponent, UserIdentifyComponent],
    imports: [
        CommonModule,
        SharedModule
    ]
})
export class PassingTestModule { }
