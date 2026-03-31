import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { CrearHuespedDto } from "../models/huesped.interface";
import { environment } from "../../../environments/environment";
import { HuespedMock } from "../models/reserva.interface";

@Injectable({
    providedIn: 'root'
})
export class HuespedService {
    private http = inject(HttpClient)
    private apiUrl = `${environment.apiUrl}/Huesped`

    registrarHuesped(huesped: CrearHuespedDto) : Observable<any> {
        return this.http.post(this.apiUrl, huesped)
    }
    getHuespedes(): Observable<HuespedMock[]> {
        return this.http.get<HuespedMock[]>(this.apiUrl);
    }
}