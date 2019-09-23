import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionOptionCreateComponent } from './question-option-create.component';

describe('QuestionOptionCreateComponent', () => {
  let component: QuestionOptionCreateComponent;
  let fixture: ComponentFixture<QuestionOptionCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuestionOptionCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionOptionCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
