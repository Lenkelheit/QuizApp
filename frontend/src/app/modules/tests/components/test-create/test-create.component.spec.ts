import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TestCreateComponent } from './test-create.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterTestingModule } from '@angular/router/testing';
import { TestService } from 'src/app/services/test.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NewTestDto } from 'src/app/models/test/new-test-dto';
import { of } from 'rxjs';
import { UserService } from 'src/app/services/user.service';

describe('TestCreateComponent', () => {
    let component: TestCreateComponent;
    let fixture: ComponentFixture<TestCreateComponent>;
    const testServiceSpy = jasmine.createSpyObj('TestService', ['createTest']);
    const userServiceSpy = jasmine.createSpyObj('UserService', ['currentUser']);

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [TestCreateComponent],
            imports: [
                BrowserAnimationsModule,
                RouterTestingModule,
                SharedModule
            ],
            providers: [
                { provide: TestService, useValue: testServiceSpy },
                { provide: UserService, useValue: userServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TestCreateComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should check "testForm" is invalid when "newTest" properties are default', () => {
        component.newTest = {} as NewTestDto;

        fixture.detectChanges();

        expect(component.testForm.valid).toBeFalse();
    });

    it('should check "testForm" is invalid when "newTest" properties have bad length', () => {
        component.newTest = {
            title: 't',
            description: 'd',
            timeLimitSeconds: '00:00:00'
        } as NewTestDto;

        fixture.detectChanges();

        expect(component.testForm.valid).toBeFalse();
    });

    it('should check "testForm" is invalid when "newTest" "timeLimitSeconds" is in bad format', () => {
        component.newTest = {
            title: 'title',
            description: 'description',
            timeLimitSeconds: '100:100:100'
        } as NewTestDto;

        fixture.detectChanges();

        expect(component.testForm.valid).toBeFalse();
    });

    it('should check "testForm" is valid when "newTest" properties have good length and are in good format', () => {
        component.newTest = {
            title: 'title',
            description: 'description',
            timeLimitSeconds: '20:30:45'
        } as NewTestDto;

        fixture.detectChanges();

        expect(component.testForm.valid).toBeTrue();
    });

    it('should send new test', () => {
        component.newTest = {} as NewTestDto;
        testServiceSpy.createTest.and.returnValue(of());
        userServiceSpy.currentUser.and.returnValue({ id: 1 });

        component.sendNewTest();

        expect(testServiceSpy.createTest).toHaveBeenCalled();
        expect(testServiceSpy.createTest).toHaveBeenCalledWith(component.newTest);
    });

    it('should clear test', () => {
        component.newTest = {
            title: 'title',
            description: 'description'
        } as NewTestDto;
        const expectedTest = {} as NewTestDto;

        component.clearTest();

        expect(component.newTest).toEqual(expectedTest);
    });
});
