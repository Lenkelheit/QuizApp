import { TestBed } from '@angular/core/testing';
import { TestResultService } from './test-result.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('TestResultService', () => {
    beforeEach(() => TestBed.configureTestingModule({
        imports: [HttpClientTestingModule]
    }));

    it('should be created', () => {
        const service: TestResultService = TestBed.get(TestResultService);
        expect(service).toBeTruthy();
    });
});
