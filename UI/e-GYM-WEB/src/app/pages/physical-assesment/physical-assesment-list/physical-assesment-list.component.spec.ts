import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicalAssesmentListComponent } from './physical-assesment-list.component';

describe('PhysicalAssesmentListComponent', () => {
  let component: PhysicalAssesmentListComponent;
  let fixture: ComponentFixture<PhysicalAssesmentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhysicalAssesmentListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicalAssesmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
