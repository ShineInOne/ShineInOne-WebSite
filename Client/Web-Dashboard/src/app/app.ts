import { Component, signal } from '@angular/core';
// import { RouterOutlet } from '@angular/router';
import { ProductsComponent } from './pages/products/products.component';

@Component({
  selector: 'app-root',
  imports: [ProductsComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ShineInONE');
}
