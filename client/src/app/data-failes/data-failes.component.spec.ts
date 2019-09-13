import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DataFailesComponent } from './data-failes.component';

describe('DataFailesComponent', () => {
  let component: DataFailesComponent;
  let fixture: ComponentFixture<DataFailesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DataFailesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DataFailesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
