import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReversalPaymentDetailsComponent } from './reversal-payment-details.component';

describe('ReversalPaymentDetailsComponent', () => {
  let component: ReversalPaymentDetailsComponent;
  let fixture: ComponentFixture<ReversalPaymentDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReversalPaymentDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReversalPaymentDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
