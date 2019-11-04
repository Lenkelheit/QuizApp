import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionOptionsFormComponent } from './question-options-form.component';

describe('QuestionOptionsFormComponent', () => {
  let component: QuestionOptionsFormComponent;
  let fixture: ComponentFixture<QuestionOptionsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuestionOptionsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionOptionsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
