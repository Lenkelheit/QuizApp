import { ErrorStateMatcher } from '@angular/material';
import { FormControl, FormGroupDirective, NgForm } from '@angular/forms';

export class ConfirmValidParentMatcher implements ErrorStateMatcher {
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        const invalidControl = control && control.invalid;

        const isEndDateLessThanStartDate = control.parent.errors ? control.parent.errors.endDateLessThanStartDate : false;
        const invalidParent = control && control.parent && control.parent.invalid && isEndDateLessThanStartDate;

        return (invalidControl || invalidParent) && (control.touched || control.dirty);
    }
}
