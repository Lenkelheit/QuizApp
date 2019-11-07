import { ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

export class FormatTimeLimitValidator {
    static validate(timeLimitReg: RegExp): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            const isFormated = timeLimitReg.test(control.value);
            return !isFormated ? { formatTimeLimit: true } : null;
        };
    }
}
