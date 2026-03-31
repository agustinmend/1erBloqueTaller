import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServicioService } from '../../core/services/servicio.service';
import { ContactoServicioDto } from '../../core/models/contacto.interface';

@Component({
  selector: 'app-contactos-servicios',
  imports: [CommonModule],
  templateUrl: './contactos-servicios.html',
  styleUrl: './contactos-servicios.css',
})
export class ContactosServicios implements OnInit {
  private servicioService = inject(ServicioService);

  contactos: ContactoServicioDto[] = [];
  cargando: boolean = true;
  mensajeError: string | null = null;

  private coloresAvatar = ['#10b981', '#f59e0b', '#ef4444', '#8b5cf6', '#06b6d4', '#3b82f6'];

  ngOnInit(): void {
    this.cargarContactos()
  }

  cargarContactos(): void {
    this.servicioService.obtenerContactos().subscribe({
      next: (data) => {
        this.contactos = data;
        this.cargando = false;
      },
      error: () => {
        this.mensajeError = 'Error al cargar el directorio de contactos.';
        this.cargando = false;
      }
    });
  }

  obtenerIniciales(nombreCompleto: string): string {
    if (!nombreCompleto) return '??';
    const partes = nombreCompleto.trim().split(' ');
    if (partes.length === 1) return partes[0].substring(0, 2).toUpperCase();
    return (partes[0][0] + partes[1][0]).toUpperCase();
  }

  obtenerColorAvatar(nombreCompleto: string): string {
    if (!nombreCompleto) return '#94a3b8'; // Gris por defecto
    let hash = 0;
    for (let i = 0; i < nombreCompleto.length; i++) {
      hash = nombreCompleto.charCodeAt(i) + ((hash << 5) - hash);
    }
    const index = Math.abs(hash) % this.coloresAvatar.length;
    return this.coloresAvatar[index];
  }
}
