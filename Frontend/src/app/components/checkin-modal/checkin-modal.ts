import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EstadiaService } from '../../core/services/estadia.services';
import { HuespedService } from '../../core/services/huesped.services';

@Component({
  selector: 'app-checkin-modal',
  imports: [CommonModule, FormsModule],
  templateUrl: './checkin-modal.html',
  styleUrl: './checkin-modal.css',
})
export class CheckinModal {
@Input() reservaId!: number;
  @Output() cerrar = new EventEmitter<void>();
  @Output() checkInExitoso = new EventEmitter<void>();

  private estadiaService = inject(EstadiaService);
  private huespedService = inject(HuespedService);

  datosReserva: any = null;
  cargando: boolean = true;
  mensajeError: string | null = null;
  
  fechaIngreso!: string;
  huespedesSeleccionados: any[] = [];
  
  huespedesDisponibles: any[] = [];
  huespedSeleccionadoId: string = '';

  ngOnInit(): void {
    const ahora = new Date();
    ahora.setMinutes(ahora.getMinutes() - ahora.getTimezoneOffset());
    this.fechaIngreso = ahora.toISOString().slice(0, 16);
    this.cargarDatos();
  }

  private cargarDatos(): void {
    this.estadiaService.obtenerPreCheckIn(this.reservaId).subscribe({
      next: (datos) => {
        this.datosReserva = datos;
        this.huespedesSeleccionados.push({
          huespedId: datos.titularId,
          nombres: datos.titularNombreCompleto,
          nroDocumentoIdentidad: datos.titularDocumento,
          esTitular: true
        });
        
        this.cargarHuespedesDropdown();
      },
      error: () => {
        this.mensajeError = "No se pudieron cargar los datos de la reserva.";
        this.cargando = false;
      }
    });
  }

  private cargarHuespedesDropdown(): void {
    this.huespedService.getHuespedes().subscribe(data => {
      this.huespedesDisponibles = data.filter((h: any) => h.huespedId !== this.datosReserva.titularId);
      this.cargando = false;
    });
  }

  get capacidadAlcanzada(): boolean {
    if (!this.datosReserva) return false;
    return this.huespedesSeleccionados.length >= this.datosReserva.capacidad;
  }

  agregarHuespedAdicional(): void {
    if (this.capacidadAlcanzada || !this.huespedSeleccionadoId) return;

    const idNum = Number(this.huespedSeleccionadoId);
    
    if (this.huespedesSeleccionados.some(h => h.huespedId === idNum)) return;

    const huespedEncontrado = this.huespedesDisponibles.find(h => h.huespedId === idNum);
    
    if (huespedEncontrado) {
      this.huespedesSeleccionados.push({
        huespedId: huespedEncontrado.huespedId,
        nombres: `${huespedEncontrado.nombres} ${huespedEncontrado.apellidos}`,
        nroDocumentoIdentidad: huespedEncontrado.nroDocumentoIdentidad,
        esTitular: false
      });
      this.huespedSeleccionadoId = '';
    }
  }

  removerHuesped(id: number): void {
    this.huespedesSeleccionados = this.huespedesSeleccionados.filter(h => h.huespedId !== id || h.esTitular);
  }

  confirmarCheckIn(): void {
    this.mensajeError = null;

    if (!this.fechaIngreso) {
      this.mensajeError = "La fecha y hora de ingreso es obligatoria.";
      return;
    }

    const payload = {
      reservaId: this.reservaId,
      fechaLlegada: new Date(this.fechaIngreso).toISOString(),
      huespedesIds: this.huespedesSeleccionados.map(h => h.huespedId)
    };

    this.estadiaService.registrarCheckIn(payload).subscribe({
      next: () => {
        this.checkInExitoso.emit();
      },
      error: (err) => {
        this.mensajeError = err.error?.error || "Error al procesar el check-in.";
      }
    });
  }
}
