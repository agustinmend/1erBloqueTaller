import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckinModal } from './checkin-modal';

describe('CheckinModal', () => {
  let component: CheckinModal;
  let fixture: ComponentFixture<CheckinModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CheckinModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CheckinModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
