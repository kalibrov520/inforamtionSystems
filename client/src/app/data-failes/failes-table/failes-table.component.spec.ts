import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FailesTableComponent } from './failes-table.component';

describe('FailesTableComponent', () => {
  let component: FailesTableComponent;
  let fixture: ComponentFixture<FailesTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FailesTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FailesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
