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


  // Mock data helpers
  getRating(id: number): number {
    // Deterministic pseudo-random based on ID
    const seed = id * 12345;
    const rating = (seed % 15) / 10 + 3.5; // Range 3.5 to 5.0
    return Math.min(5, Math.max(3.5, parseFloat(rating.toFixed(1))));
  }

  getReviewCount(id: number): number {
    const seed = id * 67890;
    return (seed % 9000) + 50; // Range 50 to 9050
  }

  getMRP(price: number): number {
    // MRP is typically 20-50% higher than selling price
    return Math.round(price * 1.4);
  }

  getDiscount(price: number): number {
    const mrp = this.getMRP(price);
    return Math.round(((mrp - price) / mrp) * 100);
  }

  // Generate an array of stars (1 for full, 0.5 for half, 0 for empty)
  getStars(rating: number): number[] {
    const stars = [];
    for (let i = 1; i <= 5; i++) {
      if (rating >= i) {
        stars.push(1);
      } else if (rating >= i - 0.5) {
        stars.push(0.5);
      } else {
        stars.push(0);
      }
    }
    return stars;
  }
}
