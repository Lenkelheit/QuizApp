import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { TestsModule } from './modules/tests/tests.module';
import { PassingTestModule } from './modules/passing-test/passing-test.module';
import { TestResultsModule } from './modules/test-results/test-results.module';
import { AuthenticationModule } from './modules/authentication/authentication.module';
import { AuthenticationInterceptorService } from './services/authentication-interceptor.service';

@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        TestsModule,
        HttpClientModule,
        BrowserAnimationsModule,
        SharedModule,
        PassingTestModule,
        TestResultsModule,
        AuthenticationModule
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthenticationInterceptorService,
            multi: true
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
