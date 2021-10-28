import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentRequestScreenComponent } from './student-request-screen.component';

describe('StudentRequestScreenComponent', () => {
  let component: StudentRequestScreenComponent;
  let fixture: ComponentFixture<StudentRequestScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentRequestScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentRequestScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
