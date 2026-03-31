import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReservaForm } from '../reserva-form/reserva-form';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-reservas',
  imports: [CommonModule, ReservaForm, RouterModule],
  templateUrl: './reservas.html',
  styleUrl: './reservas.css',
})
export class Reservas {
  mostrarModalNuevaReserva = false
  abrirModal(): void {
    this.mostrarModalNuevaReserva = true;
  }
  cerrarModal(): void {
    this.mostrarModalNuevaReserva = false;
  }

  onGuardadoExitoso(): void {
    this.cerrarModal();
  }
}
