import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationModalityFormComponent } from './registration-modality-form.component';

describe('RegistrationModalityFormComponent', () => {
  let component: RegistrationModalityFormComponent;
  let fixture: ComponentFixture<RegistrationModalityFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegistrationModalityFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationModalityFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
