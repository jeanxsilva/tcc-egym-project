import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicalAssesmentScreenComponent } from './physical-assesment-screen.component';

describe('PhysicalAssesmentScreenComponent', () => {
  let component: PhysicalAssesmentScreenComponent;
  let fixture: ComponentFixture<PhysicalAssesmentScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhysicalAssesmentScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicalAssesmentScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
