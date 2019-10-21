import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserIdentifyComponent } from './user-identify.component';

describe('UserIdentifyComponent', () => {
  let component: UserIdentifyComponent;
  let fixture: ComponentFixture<UserIdentifyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserIdentifyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserIdentifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
