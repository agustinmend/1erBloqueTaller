import { inject, Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { ReservaCreateDto } from "../models/reserva.interface";

@Injectable({ providedIn: 'root' })
export class ReservaService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/reservas`;

  crearReserva(reserva: ReservaCreateDto) {
    return this.http.post(this.apiUrl, reserva);
  }
  obtenerAgenda(termino?: string) {
    let params = new HttpParams()
    if (termino && termino.trim() !== '') {
      params = params.set('buscar', termino.trim());
    }
    return this.http.get<any[]>(`${this.apiUrl}/agenda`, {params});
  }
}
