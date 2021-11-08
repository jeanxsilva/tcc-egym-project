import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReversalPaymentScreenComponent } from './reversal-payment-screen.component';

describe('ReversalPaymentScreenComponent', () => {
  let component: ReversalPaymentScreenComponent;
  let fixture: ComponentFixture<ReversalPaymentScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReversalPaymentScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReversalPaymentScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
