import { TestBed } from '@angular/core/testing';

import { PassingTestService } from './passing-test.service';

describe('PassingTestService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PassingTestService = TestBed.get(PassingTestService);
    expect(service).toBeTruthy();
  });
});
