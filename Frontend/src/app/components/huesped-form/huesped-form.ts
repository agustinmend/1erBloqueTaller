import { Component, EventEmitter ,inject, OnInit, output, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HuespedService } from '../../core/services/huesped.services';
import { CrearHuespedDto } from '../../core/models/huesped.interface';

@Component({
  selector: 'app-huesped-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './huesped-form.html',
  styleUrl: './huesped-form.css',
})
export class HuespedForm {
  private fb = inject(FormBuilder)
  private huespedService = inject(HuespedService)

  @Output() cancelarModal = new EventEmitter<void>()
  @Output() guardadoExitoso = new EventEmitter<void>()
  huespedForm! : FormGroup
  isSubmitting = false
  errorMessage: string | null = null
  ngOnInit(): void {
    this.initForm()
  }
  private initForm(): void {
    this.huespedForm = this.fb.group({
      nombres: ['', [Validators.required, Validators.maxLength(100)]],
      apellidos: ['', [Validators.required, Validators.maxLength(100)]],
      fechaNacimiento: ['', [Validators.required]],
      nroDocumentoIdentidad: ['', [Validators.required, Validators.maxLength(20)]]
    })
  }
  onSubmit(): void {
    this.errorMessage = null
    if(this.huespedForm.invalid) {
      this.huespedForm.markAllAsTouched()
      return
    }
    this.isSubmitting = true
    const formValue = this.huespedForm.value as CrearHuespedDto
    this.huespedService.registrarHuesped(formValue).subscribe({
      next: () => {
        this.isSubmitting = false
        this.guardadoExitoso.emit()
        alert('Se registro el Huesped correctamente')
      },
      error: (errorRes) => {
        this.isSubmitting = false
        if(errorRes.status === 400 && errorRes.error?.error) {
          this.errorMessage = errorRes.error.error
        }
        else {
          this.errorMessage = 'Ocurrio un error inesperado al registrar Huesped'
        }
      }
    })
  }
  onCancelar(): void {
    this.cancelarModal.emit()
  }
}
