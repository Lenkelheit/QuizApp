export class TimeConverter {
    public static convertStringTimeToSeconds(time: string) {
        // time has format 'hh:mm:ss'
        const colons = time.split(':');

        return parseInt(colons[0]) * 60 * 60 + parseInt(colons[1]) * 60 + parseFloat(colons[2]);
    }
}
