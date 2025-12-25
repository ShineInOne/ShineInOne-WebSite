import { Component } from '@angular/core';
import { HeroComponent } from '../hero/hero.component';
import { ProductsComponent } from '../pages/products/products.component';
import { FeaturesComponent } from '../components/features/features.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeroComponent, ProductsComponent, FeaturesComponent],
  template: `
    <app-hero></app-hero>
    <app-products></app-products>
    <app-features></app-features>
  `
})
export class HomeComponent {}
