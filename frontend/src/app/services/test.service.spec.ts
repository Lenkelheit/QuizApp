import { TestBed } from '@angular/core/testing';
import { TestService } from './test.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('TestService', () => {
    beforeEach(() => TestBed.configureTestingModule({
        imports: [HttpClientTestingModule]
    }));

    it('should be created', () => {
        const service: TestService = TestBed.get(TestService);
        expect(service).toBeTruthy();
    });
});
