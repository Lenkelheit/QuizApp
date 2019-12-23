import { TestBed } from '@angular/core/testing';
import { TestEventService } from './test-event.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('TestEventService', () => {
    beforeEach(() => TestBed.configureTestingModule({
        imports: [HttpClientTestingModule]
    }));

    it('should be created', () => {
        const service: TestEventService = TestBed.get(TestEventService);
        expect(service).toBeTruthy();
    });
});
