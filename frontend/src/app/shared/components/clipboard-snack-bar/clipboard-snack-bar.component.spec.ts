import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClipboardSnackBarComponent } from './clipboard-snack-bar.component';

describe('ClipboardSnackBarComponent', () => {
  let component: ClipboardSnackBarComponent;
  let fixture: ComponentFixture<ClipboardSnackBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClipboardSnackBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClipboardSnackBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
