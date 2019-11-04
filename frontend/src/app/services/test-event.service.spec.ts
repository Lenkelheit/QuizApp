import { TestBed } from '@angular/core/testing';

import { TestEventService } from './test-event.service';

describe('TestEventService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TestEventService = TestBed.get(TestEventService);
    expect(service).toBeTruthy();
  });
});
