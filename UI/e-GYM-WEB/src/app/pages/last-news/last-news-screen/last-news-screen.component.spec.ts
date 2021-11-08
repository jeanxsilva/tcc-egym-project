import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LastNewsScreenComponent } from './last-news-screen.component';

describe('LastNewsScreenComponent', () => {
  let component: LastNewsScreenComponent;
  let fixture: ComponentFixture<LastNewsScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LastNewsScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LastNewsScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
