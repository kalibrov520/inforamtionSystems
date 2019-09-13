import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FailesComponent } from './failes.component';

describe('FailesComponent', () => {
  let component: FailesComponent;
  let fixture: ComponentFixture<FailesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FailesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FailesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
