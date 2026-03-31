import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Huespedes } from './huespedes';

describe('Huespedes', () => {
  let component: Huespedes;
  let fixture: ComponentFixture<Huespedes>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Huespedes]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Huespedes);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
