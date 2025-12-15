import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private api = 'https://localhost:7215/api/Cart';

  constructor(private http: HttpClient) {}

  // 🟢 Add a product to cart
  addToCart(userId: number, productId: number): Observable<any> {
    return this.http.post(`${this.api}/add`, {
      userId,
      productId
    });
  }

  // 🟢 Get all cart items for a user
  getCart(userId: number): Observable<any> {
    return this.http.get(`${this.api}/user/${userId}`);
  }

  // 🟢 Update quantity (+ or -)
  updateQuantity(cartItemId: number, quantity: number): Observable<any> {
    return this.http.put(`${this.api}/update`, {
      cartItemId,
      quantity
    });
  }

  // 🟢 Remove a single item from cart
  removeItem(cartItemId: number): Observable<any> {
    return this.http.delete(`${this.api}/remove/${cartItemId}`);
  }

  // 🟢 Clear whole cart (optional)
  clearCart(userId: number): Observable<any> {
    return this.http.delete(`${this.api}/clear/${userId}`);
  }
}
