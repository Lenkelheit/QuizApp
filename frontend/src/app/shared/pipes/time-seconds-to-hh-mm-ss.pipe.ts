import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'timeSecondsToHhMmSs'
})
export class TimeSecondsToHhMmSsPipe implements PipeTransform {
    transform(timeSeconds: number): string {
        if (timeSeconds == null) {
            timeSeconds = 0;
        }

        return new Date(timeSeconds * 1000).toISOString().substr(11, 8);
    }
}
