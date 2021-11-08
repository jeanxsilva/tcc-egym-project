import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LastNewsListComponent } from './last-news-list.component';

describe('LastNewsListComponent', () => {
  let component: LastNewsListComponent;
  let fixture: ComponentFixture<LastNewsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LastNewsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LastNewsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
