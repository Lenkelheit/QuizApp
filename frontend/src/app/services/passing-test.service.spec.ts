import { TestBed } from '@angular/core/testing';
import { PassingTestService } from './passing-test.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('PassingTestService', () => {
    beforeEach(() => TestBed.configureTestingModule({
        imports: [HttpClientTestingModule]
    }));

    it('should be created', () => {
        const service: PassingTestService = TestBed.get(PassingTestService);
        expect(service).toBeTruthy();
    });
});
