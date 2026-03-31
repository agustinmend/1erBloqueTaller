import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BarraLateral } from "./components/barra-lateral/barra-lateral";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, BarraLateral],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'Frontend';
}
