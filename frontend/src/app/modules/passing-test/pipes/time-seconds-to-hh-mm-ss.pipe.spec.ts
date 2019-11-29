import { TimeSecondsToHhMmSsPipe } from './time-seconds-to-hh-mm-ss.pipe';

describe('TimeSecondsToHhMmSsPipe', () => {
  it('create an instance', () => {
    const pipe = new TimeSecondsToHhMmSsPipe();
    expect(pipe).toBeTruthy();
  });
});
