import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    declarations: [UserLoginComponent],
    imports: [
        CommonModule,
        SharedModule
    ]
})
export class AuthenticationModule { }
