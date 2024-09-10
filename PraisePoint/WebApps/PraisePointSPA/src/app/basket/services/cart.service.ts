import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IProduct } from '../models/product';

import { ICart } from '../models/cart';
import { ICartItem } from '../models/cart-item';
import { HttpClient } from '@angular/common/http';
import { IAppState } from '../../shared/app-state/app-state';
import { AppStateService } from '../../shared/app-state/app-state.service';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})

export class CartService {

  private cart: Observable<ICart[]> = new Observable<ICart[]>();

  private readonly cartUrl = 'http://localhost:8001/api/v1/Basket';

  cartData: ICart = {
    username: "",
    products: [],
    total: 0,
  };
  
  cartDataObs$ = new BehaviorSubject<ICart>(this.cartData);

  constructor(private http: HttpClient) {

    const localCartData = JSON.parse(localStorage.getItem('cart') || '{}');
    if (localCartData && localCartData.products && Array.isArray(localCartData.products)) {
      this.cartData = {
        username: "",
        products: localCartData.products,
        total: localCartData.total || 0
      };
    }

    this.cartDataObs$.next(this.cartData);
  }

  addProduct(params: IProduct, username: string | undefined): void {
    const { productId, price, pictureUrl, productName } = params;
    const product: IProduct = { productId, price, pictureUrl, productName };
    
    if (!this.isProductInCart(productId)) {
      // Dodaj novi proizvod u korpu
      this.cartData.products.push({ product, quantity: 1 });
      this.cartData.total += product.price;
    } else {
      // Ažuriraj količinu postojećeg proizvoda
      this.cartData.products = this.cartData.products.map((item) => {
        if (item.product.productId === productId) {
          item.quantity += 1; // Inkrementiraj količinu
        }
        return item;
      });

      this.cartData.total = this.getCartTotal();
    }

    this.cartData.username = username;

    this.cartDataObs$.next({ ...this.cartData });
    localStorage.setItem('cart', JSON.stringify(this.cartData));

    // call endpoint
    this.updateCartBack(this.cartData).subscribe({
      next: (response) => {
        console.log('Cart successfully updated on backend for user: ' + this.cartData.username, response);
      },
      error: (err) => {
        console.error('Error updating cart on backend:', err);
      }
    });

  }

    updateCartBack(cartData: ICart): Observable<any> {
      return this.http.put(`${this.cartUrl}`, cartData); 
    }

    deleteCart(username: string | undefined): Observable<any> {
      return this.http.delete(`${this.cartUrl}/` + username);
    }

    checkout(orderData: Order): Observable<any> {
      return this.http.post(`${this.cartUrl}/Checkout`, orderData);
    }

 /* clearCart(): void {
    this.cartData = {
      products: [],
      total: 0,
    };
    this.cartDataObs$.next({ ...this.cartData });
    localStorage.setItem('cart', JSON.stringify(this.cartData));
  }*/

  removeProduct(id: string, username: string | undefined): void {
    let updatedProducts = this.cartData.products.map(cartItem => {
      if (cartItem.product.productId === id) {
        // Ako je količina veća od 1, smanjite je
        if (cartItem.quantity > 1) {
          return { ...cartItem, quantity: cartItem.quantity - 1 };
        } else {
          // Ako je količina 1, uklonite proizvod iz korpe
          return null;
        }
      }
      return cartItem;
    }).filter(cartItem => cartItem !== null); // Filtrirajte null vrednosti (koje predstavljaju uklonjene proizvode)
  
    // Ažurirajte podatke korpe
    this.cartData.products = updatedProducts.filter(product => product !== null) as ICartItem[];
    this.cartData.total = this.getCartTotal();

    this.cartData.username = username;
  
    // Obavestite pretplatnike o promenama
    this.cartDataObs$.next({ ...this.cartData });
  
    // Sačuvajte ažuriranu korpu u localStorage
    localStorage.setItem('cart', JSON.stringify(this.cartData));

    // call endpoint
    this.updateCartBack(this.cartData).subscribe({
      next: (response) => {
        console.log('Cart successfully updated on backend for user: ' + this.cartData.username, response);
      },
      error: (err) => {
        console.error('Error updating cart on backend:', err);
      }
    });

    // this._notification.create(
    //   'success',
    //   'Removed successfully',
    //   'The selected item was removed from the cart successfully'
    // );
  }

  updateCart(id: string, quantity: number): void {
    // copy array, find item index and update
    let updatedProducts = [...this.cartData.products];
    let productIndex = updatedProducts.findIndex((prod) => prod.product.productId == id);

    updatedProducts[productIndex] = {
      ...updatedProducts[productIndex],
      quantity: quantity,
    };

    this.cartData.products = updatedProducts;
    this.cartData.total = this.getCartTotal();
    this.cartDataObs$.next({ ...this.cartData });
    localStorage.setItem('cart', JSON.stringify(this.cartData));

    // call endpoint
    this.updateCartBack(this.cartData).subscribe({
      next: (response) => {
        console.log('Cart successfully updated on backend for user: ' + this.cartData.username, response);
      },
      error: (err) => {
        console.error('Error updating cart on backend:', err);
      }
    });
  }

  isProductInCart(id: string): boolean {
    return this.cartData.products.findIndex((prod) => prod.product.productId === id) !== -1;
  }

  getCartTotal(): number {
    let totalSum = 0;
    this.cartData.products.forEach(
      (prod) => (totalSum += prod.product.price * prod.quantity)
    );

    return totalSum;
  }
}

