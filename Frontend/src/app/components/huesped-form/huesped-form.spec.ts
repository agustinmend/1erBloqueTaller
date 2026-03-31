import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HuespedForm } from './huesped-form';

describe('HuespedForm', () => {
  let component: HuespedForm;
  let fixture: ComponentFixture<HuespedForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HuespedForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HuespedForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
