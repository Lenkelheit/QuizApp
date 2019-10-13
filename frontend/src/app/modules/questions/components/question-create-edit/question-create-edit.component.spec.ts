import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionCreateEditComponent } from './question-create-edit.component';

describe('QuestionCreateEditComponent', () => {
    let component: QuestionCreateEditComponent;
    let fixture: ComponentFixture<QuestionCreateEditComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [QuestionCreateEditComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(QuestionCreateEditComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
