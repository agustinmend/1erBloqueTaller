import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { FormsModule, FormGroup, Validators, ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReservaCreateDto, VariacionHabitacionMock, HuespedMock } from '../../core/models/reserva.interface';
import { ReservaService } from '../../core/services/reserva.services';
import { HuespedService } from '../../core/services/huesped.services';


@Component({
  selector: 'app-reserva-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './reserva-form.html',
  styleUrl: './reserva-form.css',
})
export class ReservaForm implements OnInit{
  private fb = inject(FormBuilder)
  private huespedService = inject(HuespedService)
  private reservaService = inject(ReservaService)
  @Output() cancelarModal = new EventEmitter<void>();
  @Output() guardadoExitoso = new EventEmitter<void>();
  
  reservaForm!: FormGroup;
  listaHuespedes: any[] = [];
  cargandoHuespedes = true;
  mensajeError: string | null = null;

  ngOnInit(): void {
    this.cargarHuespedes();
    this.initForm();
  }

  private initForm() {
    this.reservaForm = this.fb.group({
      titularId: ['', Validators.required],
      fechaInicio: ['', Validators.required],
      fechaFin: ['', Validators.required],
      estado: ['Confirmada', Validators.required],
      tipoHabitacion: ['', Validators.required],
      cantidadPersonas: [1, [Validators.required, Validators.min(1)]]
    });
  }

  private cargarHuespedes() {
    this.huespedService.getHuespedes().subscribe({
      next: (data) => {
        this.listaHuespedes = data;
        this.cargandoHuespedes = false;
      },
      error: () => this.mensajeError = 'Error al cargar huéspedes.'
    });
  }

  enviar() {
    if (this.reservaForm.invalid) {
      this.reservaForm.markAllAsTouched();
      return;
    }

    this.reservaService.crearReserva(this.reservaForm.value).subscribe({
      next: () => {
        this.guardadoExitoso.emit()
        alert('reserva guardada exitosamente')
      },
      error: (err) => {
        if (err.error) {
          if (err.error.error && typeof err.error.error === 'string') {
            this.mensajeError = err.error.error;
          } 
          else if (err.error.errors) {
            const camposConError = Object.keys(err.error.errors);
            if (camposConError.length > 0) {
              const primerCampo = camposConError[0];
              this.mensajeError = err.error.errors[primerCampo][0];
            }
          } 
          else if (typeof err.error === 'string') {
             this.mensajeError = err.error;
          }
        } 

        if (!this.mensajeError) {
          this.mensajeError = 'Ocurrió un error de conexión con el servidor.';
        }
      }
    });
  }
}
