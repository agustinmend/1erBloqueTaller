import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class EstadiaService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/estadias`;

  obtenerPreCheckIn(reservaId: number) {
    return this.http.get<any>(`${this.apiUrl}/pre-checkin/${reservaId}`);
  }

  registrarCheckIn(payload: any) {
    return this.http.post(`${this.apiUrl}/checkin`, payload);
  }
}