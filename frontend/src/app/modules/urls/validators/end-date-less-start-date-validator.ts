import { FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

export class EndDateLessStartDateValidator {
    static validate: ValidatorFn = (group: FormGroup): ValidationErrors | null => {
        const validFromTime = group.get('validFromTime');
        const validUntilTime = group.get('validUntilTime');

        return validFromTime && validUntilTime && validFromTime.value > validUntilTime.value ? { endDateLessThanStartDate: true } : null;
    }
}
