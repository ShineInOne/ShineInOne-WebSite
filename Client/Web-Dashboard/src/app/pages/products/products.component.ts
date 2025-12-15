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
  topProducts: any[] = [];
  userId = 1; // temporary until login is implemented

  constructor(
    private productService: ProductService,
    private cartService: CartService
  ) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts()
      .subscribe(data => {
        this.products = data;
        this.topProducts = data.filter(p => p.categoryId === 1);
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

  scroll(container: HTMLElement, direction: 'left' | 'right') {
    const scrollAmount = 300; // Adjust scroll distance as needed
    if (direction === 'left') {
      container.scrollBy({ left: -scrollAmount, behavior: 'smooth' });
    } else {
      container.scrollBy({ left: scrollAmount, behavior: 'smooth' });
    }
  }

}
