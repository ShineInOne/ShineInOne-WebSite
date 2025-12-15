import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../services/cart.service';

@Component({
  standalone: true,
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  imports: [CommonModule]
})
export class CartComponent implements OnInit {

  userId = 1; // temp dummy user until login
  cartItems: any[] = [];

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart() {
    this.cartService.getCart(this.userId).subscribe(res => {
      this.cartItems = res;
    });
  }

  updateQty(item: any, change: number) {
    const newQty = item.quantity + change;

    if (newQty <= 0) {
      this.removeItem(item.id);
      return;
    }

    this.cartService.updateQuantity(item.id, newQty).subscribe(() => {
      this.loadCart();
    });
  }

  removeItem(cartItemId: number) {
    this.cartService.removeItem(cartItemId).subscribe(() => {
      this.loadCart();
    });
  }

  getTotal() {
    return this.cartItems.reduce((sum, item) => {
      return sum + (item.product.price * item.quantity);
    }, 0);
  }
}
