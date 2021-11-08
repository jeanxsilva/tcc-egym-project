import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LastNewsCardComponent } from './last-news-card.component';

describe('LastNewsCardComponent', () => {
  let component: LastNewsCardComponent;
  let fixture: ComponentFixture<LastNewsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LastNewsCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LastNewsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
