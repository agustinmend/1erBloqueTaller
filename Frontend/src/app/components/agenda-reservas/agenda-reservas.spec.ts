import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgendaReservas } from './agenda-reservas';

describe('AgendaReservas', () => {
  let component: AgendaReservas;
  let fixture: ComponentFixture<AgendaReservas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AgendaReservas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgendaReservas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
