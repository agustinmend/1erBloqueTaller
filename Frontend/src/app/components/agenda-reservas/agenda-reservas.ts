import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReservaService } from '../../core/services/reserva.services';
import { CheckinModal } from '../checkin-modal/checkin-modal';

@Component({
  selector: 'app-agenda-reservas',
  imports: [CommonModule, FormsModule, CheckinModal],
  templateUrl: './agenda-reservas.html',
  styleUrl: './agenda-reservas.css',
})
export class AgendaReservas implements OnInit{
  private reservaService = inject(ReservaService);

  reservas: any[] = [];
  reservasFiltradas: any[] = [];
  terminoBusqueda: string = '';
  cargando: boolean = true;

  reservaParaCheckIn: number | null = null

  ngOnInit(): void {
    this.cargarAgenda();
  }

  cargarAgenda(termino?: string): void {
    this.cargando = true
    this.reservaService.obtenerAgenda(termino).subscribe({
      next: (data) => {
        this.reservas = data;
        this.reservasFiltradas = data;
        this.cargando = false;
      },
      error: (err) => {
        console.error('Error al cargar la agenda', err);
        this.cargando = false;
      }
    });
  }

  filtrarReservas(): void {
    this.cargarAgenda(this.terminoBusqueda)
  }
  limpiarFiltro(): void {
    this.terminoBusqueda = '';
    this.cargarAgenda();
  }
  
  abrirModalCheckIn(idReserva: number): void {
    this.reservaParaCheckIn = idReserva;
  }

  onCheckInExitoso(): void {
    this.reservaParaCheckIn = null;
    this.cargarAgenda();
  }
}
