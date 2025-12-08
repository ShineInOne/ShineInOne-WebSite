import { Component, signal } from '@angular/core';
// import { RouterOutlet } from '@angular/router';
import { ProductsComponent } from './pages/products/products.component';
import { HeroComponent } from './hero/hero.component';

@Component({
  selector: 'app-root',
  imports: [HeroComponent, ProductsComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ShineInONE');
}
