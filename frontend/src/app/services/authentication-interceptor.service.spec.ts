import { TestBed } from '@angular/core/testing';
import { AuthenticationInterceptorService } from './authentication-interceptor.service';
import { RouterTestingModule } from '@angular/router/testing';

describe('AuthenticationInterceptorService', () => {
    beforeEach(() => TestBed.configureTestingModule({
        imports: [RouterTestingModule],
        providers: [AuthenticationInterceptorService]
    }));

    it('should be created', () => {
        const service: AuthenticationInterceptorService = TestBed.get(AuthenticationInterceptorService);
        expect(service).toBeTruthy();
    });
});
