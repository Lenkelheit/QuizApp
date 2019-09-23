import { TestBed } from '@angular/core/testing';

import { QuestionOptionService } from './question-option.service';

describe('QuestionOptionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: QuestionOptionService = TestBed.get(QuestionOptionService);
    expect(service).toBeTruthy();
  });
});
