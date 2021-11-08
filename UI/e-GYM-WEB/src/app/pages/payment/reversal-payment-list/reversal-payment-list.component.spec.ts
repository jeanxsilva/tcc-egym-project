import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReversalPaymentListComponent } from './reversal-payment-list.component';

describe('ReversalPaymentListComponent', () => {
  let component: ReversalPaymentListComponent;
  let fixture: ComponentFixture<ReversalPaymentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReversalPaymentListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReversalPaymentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
