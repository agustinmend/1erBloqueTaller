import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { ContactoServicioDto } from "../models/contacto.interface";

@Injectable({ providedIn: 'root' })
export class ServicioService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Departamentos`;

  obtenerContactos() {
    return this.http.get<ContactoServicioDto[]>(`${this.apiUrl}/contactos`);
  }
}