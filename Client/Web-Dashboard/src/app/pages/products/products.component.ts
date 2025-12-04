import { Component,OnInit,signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent{

  products : any = [];

  constructor(private service: ProductService) { }

  ngOnInit(): void {
    this.service.getProducts()
      .subscribe(data => {
        this.products = data
      });
  }
}
