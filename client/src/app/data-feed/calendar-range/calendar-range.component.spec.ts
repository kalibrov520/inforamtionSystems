import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarRangeComponent } from './calendar-range.component';

describe('CalendarComponent', () => {
  let component: CalendarRangeComponent;
  let fixture: ComponentFixture<CalendarRangeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarRangeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarRangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
