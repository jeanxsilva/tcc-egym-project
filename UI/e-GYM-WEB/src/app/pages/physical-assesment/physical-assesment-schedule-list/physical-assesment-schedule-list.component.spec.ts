import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicalAssesmentScheduleListComponent } from './physical-assesment-schedule-list.component';

describe('PhysicalAssesmentScheduleListComponent', () => {
  let component: PhysicalAssesmentScheduleListComponent;
  let fixture: ComponentFixture<PhysicalAssesmentScheduleListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhysicalAssesmentScheduleListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicalAssesmentScheduleListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
