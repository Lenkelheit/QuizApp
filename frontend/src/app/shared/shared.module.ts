import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatInputModule, MatIconModule, MatSelectModule, MatTabsModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatPaginatorModule } from '@angular/material/paginator';
import { TopBarComponent } from './components/top-bar/top-bar.component';
import { MatStepperModule } from '@angular/material/stepper';
import { TimeSecondsToHhMmSsPipe } from './pipes/time-seconds-to-hh-mm-ss.pipe';
import { ClipboardSnackBarComponent } from './components/clipboard-snack-bar/clipboard-snack-bar.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

@NgModule({
    declarations: [TopBarComponent, TimeSecondsToHhMmSsPipe, ClipboardSnackBarComponent, PageNotFoundComponent],
    entryComponents: [ClipboardSnackBarComponent],
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        MatTableModule,
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        MatIconModule,
        MatToolbarModule,
        ReactiveFormsModule,
        MatCheckboxModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatSelectModule,
        MatTabsModule,
        MatStepperModule,
        MatPaginatorModule,
        MatSnackBarModule
    ],
    exports: [
        TopBarComponent,
        TimeSecondsToHhMmSsPipe,
        ClipboardSnackBarComponent,
        RouterModule,
        FormsModule,
        MatTableModule,
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        MatIconModule,
        MatToolbarModule,
        ReactiveFormsModule,
        MatCheckboxModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatSelectModule,
        MatTabsModule,
        MatStepperModule,
        MatPaginatorModule,
        MatSnackBarModule
    ]
})
export class SharedModule { }
