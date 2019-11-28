import { Component, OnInit } from '@angular/core';
import { MatSnackBarRef } from '@angular/material';

@Component({
    selector: 'app-clipboard-snack-bar',
    templateUrl: './clipboard-snack-bar.component.html',
    styleUrls: ['./clipboard-snack-bar.component.css']
})
export class ClipboardSnackBarComponent {

    constructor(public snackBarRef: MatSnackBarRef<ClipboardSnackBarComponent>) { }
}
