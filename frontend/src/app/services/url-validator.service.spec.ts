import { TestBed } from '@angular/core/testing';

import { UrlValidatorService } from './url-validator.service';

describe('UrlValidatorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UrlValidatorService = TestBed.get(UrlValidatorService);
    expect(service).toBeTruthy();
  });
});
