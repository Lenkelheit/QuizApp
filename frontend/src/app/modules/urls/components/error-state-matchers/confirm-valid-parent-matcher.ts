import { ErrorStateMatcher } from '@angular/material';
import { FormControl, FormGroupDirective, NgForm } from '@angular/forms';

export class ConfirmValidParentMatcher implements ErrorStateMatcher {
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        const invalidControl = control && control.invalid;
        const invalidParent = control && control.parent && control.parent.invalid;

        return (invalidControl || invalidParent) && (control.touched || control.dirty);
    }
}
