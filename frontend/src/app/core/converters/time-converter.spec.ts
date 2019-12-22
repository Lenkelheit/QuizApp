import { TimeConverter } from './time-converter';

describe('TimeConverter', () => {
    it('should create an instance', () => {
        expect(new TimeConverter()).toBeTruthy();
    });

    it('should convert string time to seconds', () => {
        const time = '00:00:45';
        const expectedTimeSeconds = 45;

        const actualTimeSeconds = TimeConverter.convertStringTimeToSeconds(time);

        expect(actualTimeSeconds).toBe(expectedTimeSeconds);
    });

    it('should convert null as "time" argument to seconds', () => {
        const time = null;
        const expectedTimeSeconds = 0;

        const actualTimeSeconds = TimeConverter.convertStringTimeToSeconds(time);

        expect(actualTimeSeconds).toBe(expectedTimeSeconds);
    });
});
