import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products: any[] = [];
  userId = 1; // temporary until login is implemented

  constructor(
    private productService: ProductService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts()
      .subscribe(data => {
        this.products = data;
      });
  }

  addToCart(productId: number) {
    this.cartService.addToCart(this.userId, productId).subscribe({
      next: (res) => {
        console.log("Added to cart:", res);
        alert("Product added to cart!");
      },
      error: (err) => {
        console.error("Cart error:", err);
        alert("Could not add to cart");
      }
    });
  }

}
