import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalityStudentsComponent } from './modality-students.component';

describe('ModalityStudentsComponent', () => {
  let component: ModalityStudentsComponent;
  let fixture: ComponentFixture<ModalityStudentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalityStudentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalityStudentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
