import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { QuestionFormComponent } from './question-form.component';
import { ViewQuestionDto } from 'src/app/models/question/view-question-dto';
import { PassingTestModule } from '../../passing-test.module';
import { TestEventService } from 'src/app/services/test-event.service';
import { Observable } from 'rxjs';
import { ViewQuestionOptionDto } from 'src/app/models/question-option/view-question-option-dto';

describe('QuestionFormComponent', () => {
    let component: QuestionFormComponent;
    let fixture: ComponentFixture<QuestionFormComponent>;
    const testEventServiceSpy = {};

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                PassingTestModule
            ],
            providers: [
                { provide: TestEventService, useValue: testEventServiceSpy }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(QuestionFormComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.viewQuestion = {} as ViewQuestionDto;
        component.sendQuestion$ = new Observable();

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should set "questionTimeLimitSeconds"', () => {
        const viewQuestion = {
            timeLimitSeconds: '00:00:45'
        } as ViewQuestionDto;
        component.viewQuestion = viewQuestion;
        component.sendQuestion$ = new Observable();

        fixture.detectChanges();

        expect(component.questionTimeLimitSeconds).toBe(45);
    });

    it('should set option by index', () => {
        const viewQuestion = {
            testQuestionOptions: [] as ViewQuestionOptionDto[]
        } as ViewQuestionDto;
        component.viewQuestion = viewQuestion;
        component.sendQuestion$ = new Observable();
        fixture.detectChanges();
        const optionIndex = 1;
        const option = {} as ViewQuestionOptionDto;

        component.setOption(optionIndex, option);

        expect(component.viewQuestion.testQuestionOptions[optionIndex]).toBe(option);
    });
});
