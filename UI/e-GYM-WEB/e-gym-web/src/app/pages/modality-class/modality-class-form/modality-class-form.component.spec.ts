import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalityClassFormComponent } from './modality-class-form.component';

describe('ModalityClassFormComponent', () => {
  let component: ModalityClassFormComponent;
  let fixture: ComponentFixture<ModalityClassFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalityClassFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalityClassFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
