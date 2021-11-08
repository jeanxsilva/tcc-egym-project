import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentMovementFormComponent } from './payment-movement-form.component';

describe('PaymentMovementFormComponent', () => {
  let component: PaymentMovementFormComponent;
  let fixture: ComponentFixture<PaymentMovementFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentMovementFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaymentMovementFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
