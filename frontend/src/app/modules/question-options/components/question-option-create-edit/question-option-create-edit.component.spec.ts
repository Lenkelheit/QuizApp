import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionOptionCreateEditComponent } from './question-option-create-edit.component';

describe('QuestionOptionCreateEditComponent', () => {
    let component: QuestionOptionCreateEditComponent;
    let fixture: ComponentFixture<QuestionOptionCreateEditComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [QuestionOptionCreateEditComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(QuestionOptionCreateEditComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
