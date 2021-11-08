import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingPlanFormComponent } from './training-plan-form.component';

describe('TrainingPlanFormComponent', () => {
  let component: TrainingPlanFormComponent;
  let fixture: ComponentFixture<TrainingPlanFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingPlanFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingPlanFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
