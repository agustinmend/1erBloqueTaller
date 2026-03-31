export interface ReservaCreateDto {
  titularId: number;
  fechaInicio: string;
  fechaFin: string;
  tipoHabitacion: string;
  cantidadPersonas: number;
  estado: string;
}

export interface VariacionHabitacionMock {
  tipo: string;
  capacidadBase: number;
  precioPorNoche: number;
  descripcion: string;
}

export interface HuespedMock {
  id: number;
  nombreCompleto: string;
  documento: string;
}