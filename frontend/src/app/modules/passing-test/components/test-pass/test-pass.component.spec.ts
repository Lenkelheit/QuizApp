import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TestPassComponent } from './test-pass.component';
import { Component, Input } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { IdentityUrlDto } from 'src/app/models/url-validator/identity-url-dto';

@Component({ selector: 'app-user-identify', template: '' })
class UserIdentifyStubComponent { }

@Component({ selector: 'app-test-form', template: '' })
class TestFormStubComponent {
    @Input() getIdentityUrl$: any;
}

describe('TestPassComponent', () => {
    let component: TestPassComponent;
    let fixture: ComponentFixture<TestPassComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                TestPassComponent,
                UserIdentifyStubComponent,
                TestFormStubComponent
            ],
            imports: [
                RouterTestingModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TestPassComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should set that user is identified', () => {
        component.setIdentityUrl({} as IdentityUrlDto);

        expect(component.isUserIdentified).toBeTrue();
    });

    it('should set test result id and test is sent', () => {
        const testResultId = 1;

        component.setTestResultId(testResultId);

        expect(component.testResultId).toBe(testResultId);
        expect(component.istestSent).toBeTrue();
    });
});
