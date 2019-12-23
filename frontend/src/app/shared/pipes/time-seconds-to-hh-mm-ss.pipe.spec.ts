import { TimeSecondsToHhMmSsPipe } from './time-seconds-to-hh-mm-ss.pipe';

describe('TimeSecondsToHhMmSsPipe', () => {
    const pipe = new TimeSecondsToHhMmSsPipe();

    it('create an instance', () => {
        expect(pipe).toBeTruthy();
    });

    it('transforms 0 seconds to "00:00:00"', () => {
        expect(pipe.transform(0)).toBe('00:00:00');
    });

    it('transforms null to "00:00:00"', () => {
        expect(pipe.transform(null)).toBe('00:00:00');
    });

    it('transforms 100 seconds to "00:01:40"', () => {
        expect(pipe.transform(100)).toBe('00:01:40');
    });
});
