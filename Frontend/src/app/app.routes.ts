import { Routes } from '@angular/router';
import { Huespedes } from './components/huespedes/huespedes';
import { Inicio } from './components/inicio/inicio';
import { Reservas } from './components/reservas/reservas';
import { AgendaReservas } from './components/agenda-reservas/agenda-reservas';
import { ContactosServicios } from './components/contactos-servicios/contactos-servicios';
import { Servicios } from './components/servicios/servicios';
export const routes: Routes = [
    {path: '', redirectTo: 'inicio', pathMatch: 'full'},
    {path: 'inicio', component:Inicio},
    {path: 'huespedes', component: Huespedes},
    {path: 'reservas', component: Reservas},
    {path: 'reservas/agenda', component: AgendaReservas},
    {path: 'servicios', component:Servicios},
    {path: 'servicios/contactos', component: ContactosServicios}
];
