import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchedulePhysicalAssesmentComponent } from './schedule-physical-assesment.component';

describe('SchedulePhysicalAssesmentComponent', () => {
  let component: SchedulePhysicalAssesmentComponent;
  let fixture: ComponentFixture<SchedulePhysicalAssesmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SchedulePhysicalAssesmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SchedulePhysicalAssesmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
