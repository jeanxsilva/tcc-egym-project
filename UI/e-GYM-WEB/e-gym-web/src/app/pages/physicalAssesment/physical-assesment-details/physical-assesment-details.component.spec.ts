import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicalAssesmentDetailsComponent } from './physical-assesment-details.component';

describe('PhysicalAssesmentDetailsComponent', () => {
  let component: PhysicalAssesmentDetailsComponent;
  let fixture: ComponentFixture<PhysicalAssesmentDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhysicalAssesmentDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicalAssesmentDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
