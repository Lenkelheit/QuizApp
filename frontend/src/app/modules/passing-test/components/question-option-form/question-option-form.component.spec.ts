import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionOptionFormComponent } from './question-option-form.component';

describe('QuestionOptionFormComponent', () => {
  let component: QuestionOptionFormComponent;
  let fixture: ComponentFixture<QuestionOptionFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuestionOptionFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionOptionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
