import { TestBed } from '@angular/core/testing';
import { UrlValidatorService } from './url-validator.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('UrlValidatorService', () => {
    beforeEach(() => TestBed.configureTestingModule({
        imports: [HttpClientTestingModule]
    }));

    it('should be created', () => {
        const service: UrlValidatorService = TestBed.get(UrlValidatorService);
        expect(service).toBeTruthy();
    });
});
