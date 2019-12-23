import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { QuestionOptionComponent } from './question-option.component';
import { SharedModule } from 'src/app/shared/shared.module';

describe('QuestionOptionComponent', () => {
    let component: QuestionOptionComponent;
    let fixture: ComponentFixture<QuestionOptionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [QuestionOptionComponent],
            imports: [
                SharedModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(QuestionOptionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
