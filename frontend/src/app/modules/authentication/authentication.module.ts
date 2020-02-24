import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { UserSignupComponent } from './components/user-signup/user-signup.component';

@NgModule({
    declarations: [UserLoginComponent, UserSignupComponent],
    imports: [
        CommonModule,
        SharedModule
    ]
})
export class AuthenticationModule { }
