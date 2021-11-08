import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingPlanListComponent } from './training-plan-list.component';

describe('TrainingPlanListComponent', () => {
  let component: TrainingPlanListComponent;
  let fixture: ComponentFixture<TrainingPlanListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingPlanListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingPlanListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
