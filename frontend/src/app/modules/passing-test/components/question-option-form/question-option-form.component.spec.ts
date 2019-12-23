import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { QuestionOptionFormComponent } from './question-option-form.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Observable } from 'rxjs';
import { ViewQuestionOptionDto } from 'src/app/models/question-option/view-question-option-dto';

describe('QuestionOptionFormComponent', () => {
    let component: QuestionOptionFormComponent;
    let fixture: ComponentFixture<QuestionOptionFormComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [QuestionOptionFormComponent],
            imports: [
                SharedModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(QuestionOptionFormComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.getOption$ = new Observable();
        component.viewQuestionOption = {} as ViewQuestionOptionDto;

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });
});
