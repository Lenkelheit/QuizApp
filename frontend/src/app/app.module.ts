import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { TestsModule } from './modules/tests/tests.module';

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
        SharedModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
