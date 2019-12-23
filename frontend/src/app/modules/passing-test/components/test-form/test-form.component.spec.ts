import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TestFormComponent } from './test-form.component';
import { PassingTestModule } from '../../passing-test.module';
import { TestEventService } from 'src/app/services/test-event.service';
import { TestService } from 'src/app/services/test.service';
import { PassingTestService } from 'src/app/services/passing-test.service';
import { Observable, of } from 'rxjs';
import { IdentityUrlDto } from 'src/app/models/url-validator/identity-url-dto';
import { UserUrlDto } from 'src/app/models/passing-test/user-url-dto';

describe('TestFormComponent', () => {
    let component: TestFormComponent;
    let fixture: ComponentFixture<TestFormComponent>;
    const testEventServiceSpy = {};
    const testServiceSpy = jasmine.createSpyObj('TestService', ['getPassingTestById']);
    const passingTestServiceSpy = jasmine.createSpyObj('PassingTestService', ['createTestResult']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                PassingTestModule
            ],
            providers: [
                { provide: TestEventService, useValue: testEventServiceSpy },
                { provide: TestService, useValue: testServiceSpy },
                { provide: PassingTestService, useValue: passingTestServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TestFormComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.getIdentityUrl$ = new Observable();

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should send test and create test result', () => {
        const identityUrl = {
            id: 1
        } as IdentityUrlDto;
        component.getIdentityUrl$ = of(identityUrl);
        testServiceSpy.getPassingTestById.and.returnValue(of());
        fixture.detectChanges();
        const userUrl = {
            urlId: identityUrl.id,
            sessionId: component.sessionId
        } as UserUrlDto;
        passingTestServiceSpy.createTestResult.and.returnValue(of());

        component.sendTest();

        expect(passingTestServiceSpy.createTestResult).toHaveBeenCalled();
        expect(passingTestServiceSpy.createTestResult).toHaveBeenCalledWith(userUrl);
    });
});
