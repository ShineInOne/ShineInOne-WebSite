import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ProductsComponent } from './pages/products/products.component';
import { HeroComponent } from './hero/hero.component';
import { HeaderComponent } from "./components/header/header.component";
import { FooterComponent } from "./components/footer/footer.component";
import { FeaturesComponent } from "./components/features/features.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeroComponent, ProductsComponent, HeaderComponent, FooterComponent, FeaturesComponent, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ShineInONE');
}
