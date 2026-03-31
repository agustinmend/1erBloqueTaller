import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactosServicios } from './contactos-servicios';

describe('ContactosServicios', () => {
  let component: ContactosServicios;
  let fixture: ComponentFixture<ContactosServicios>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactosServicios]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContactosServicios);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
