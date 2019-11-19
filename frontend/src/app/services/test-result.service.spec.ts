import { TestBed } from '@angular/core/testing';

import { TestResultService } from './test-result.service';

describe('TestResultService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TestResultService = TestBed.get(TestResultService);
    expect(service).toBeTruthy();
  });
});
