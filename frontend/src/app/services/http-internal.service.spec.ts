import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { HttpInternalService } from './http-internal.service';

describe('HttpInternalService', () => {
    beforeEach(() => TestBed.configureTestingModule({
        imports: [HttpClientTestingModule]
    }));

    it('should be created', () => {
        const service: HttpInternalService = TestBed.get(HttpInternalService);
        expect(service).toBeTruthy();
    });
});
