import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { QuestionOptionCreateEditComponent } from './question-option-create-edit.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Observable } from 'rxjs';
import { UpdateQuestionOptionDto } from 'src/app/models/question-option/update-question-option-dto';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('QuestionOptionCreateEditComponent', () => {
    let component: QuestionOptionCreateEditComponent;
    let fixture: ComponentFixture<QuestionOptionCreateEditComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [QuestionOptionCreateEditComponent],
            imports: [
                BrowserAnimationsModule,
                SharedModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(QuestionOptionCreateEditComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.getOption$ = new Observable();
        component.updateQuestionOption = {} as UpdateQuestionOptionDto;

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });

    it('should check "questionOptionForm" is invalid when "updateQuestionOption" properties are default', () => {
        component.getOption$ = new Observable();
        component.updateQuestionOption = {} as UpdateQuestionOptionDto;

        fixture.detectChanges();

        expect(component.questionOptionForm.valid).toBeFalse();
    });

    it('should check "questionOptionForm" is invalid when "updateQuestionOption" properties have bad length', () => {
        component.getOption$ = new Observable();
        component.updateQuestionOption = {
            text: 't',
            isRight: true
        } as UpdateQuestionOptionDto;

        fixture.detectChanges();

        expect(component.questionOptionForm.valid).toBeFalse();
    });

    it('should check "questionOptionForm" is valid when "updateQuestionOption" properties have good length', () => {
        component.getOption$ = new Observable();
        component.updateQuestionOption = {
            text: 'text',
            isRight: true
        } as UpdateQuestionOptionDto;

        fixture.detectChanges();

        expect(component.questionOptionForm.valid).toBeTrue();
    });
});
