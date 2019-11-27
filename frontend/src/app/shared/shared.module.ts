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
import { TopBarComponent } from './components/top-bar/top-bar.component';
import { MatStepperModule } from '@angular/material/stepper';
import { ClipboardSnackBarComponent } from './components/clipboard-snack-bar/clipboard-snack-bar.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
    declarations: [TopBarComponent, ClipboardSnackBarComponent],
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
        MatSnackBarModule
    ],
    exports: [
        TopBarComponent,
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
        MatSnackBarModule
    ]
})
export class SharedModule { }
