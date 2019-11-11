import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestUrlsComponent } from './test-urls.component';

describe('TestUrlsComponent', () => {
  let component: TestUrlsComponent;
  let fixture: ComponentFixture<TestUrlsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestUrlsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestUrlsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
