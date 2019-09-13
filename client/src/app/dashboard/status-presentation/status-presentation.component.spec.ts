import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatusPresentationComponent } from './status-presentation.component';

describe('StatusPresentationComponent', () => {
  let component: StatusPresentationComponent;
  let fixture: ComponentFixture<StatusPresentationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatusPresentationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatusPresentationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
