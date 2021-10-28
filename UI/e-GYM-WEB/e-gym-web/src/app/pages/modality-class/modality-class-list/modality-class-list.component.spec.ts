import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalityClassListComponent } from './modality-class-list.component';

describe('ModalityClassListComponent', () => {
  let component: ModalityClassListComponent;
  let fixture: ComponentFixture<ModalityClassListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalityClassListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalityClassListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
