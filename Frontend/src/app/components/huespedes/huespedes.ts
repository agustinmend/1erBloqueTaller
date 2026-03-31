import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HuespedForm } from '../huesped-form/huesped-form';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-huespedes',
  imports: [CommonModule, HuespedForm, RouterModule],
  templateUrl: './huespedes.html',
  styleUrl: './huespedes.css',
})
export class Huespedes {
  mostrarModal = false;

  huespedesEstaticos = [
    { documento: '12345678', nombreCompleto: 'María González López', fechaNacimiento: '14 de marzo de 1985' },
    { documento: '87654321', nombreCompleto: 'Juan Carlos Rodríguez Pérez', fechaNacimiento: '21 de julio de 1990' },
    { documento: '45678912', nombreCompleto: 'Ana Patricia Martínez Silva', fechaNacimiento: '7 de noviembre de 1988' },
    { documento: '98765432', nombreCompleto: 'Carlos Alberto Fernández Ruiz', fechaNacimiento: '29 de mayo de 1992' },
    { documento: '32165498', nombreCompleto: 'Laura Elena Sánchez Torres', fechaNacimiento: '11 de septiembre de 1987' }
  ];

  abrirModal(): void {
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
  }

  onGuardadoExitoso(): void {
    this.cerrarModal();
  }
}
