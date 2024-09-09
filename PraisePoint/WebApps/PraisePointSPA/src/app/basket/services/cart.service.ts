import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IProduct } from '../models/product';

import { ICart } from '../models/cart';
import { ICartItem } from '../models/cart-item';

@Injectable({
  providedIn: 'root'
})

export class CartService {

  cartData: ICart = {
    products: [],
    total: 0,
  };
  
  cartDataObs$ = new BehaviorSubject<ICart>(this.cartData);

  constructor() {
    const localCartData = JSON.parse(localStorage.getItem('cart') || '{}');
    if (localCartData && localCartData.products && Array.isArray(localCartData.products)) {
      this.cartData = {
        products: localCartData.products,
        total: localCartData.total || 0
      };
    }

    this.cartDataObs$.next(this.cartData);
  }

  addProduct(params: IProduct): void {
    const { id, price, image, name } = params;
    const product: IProduct = { id, price, image, name };
    
    if (!this.isProductInCart(id)) {
      // Dodaj novi proizvod u korpu
      this.cartData.products.push({ product, quantity: 1 });
      this.cartData.total += product.price;
    } else {
      // Ažuriraj količinu postojećeg proizvoda
      this.cartData.products = this.cartData.products.map((item) => {
        if (item.product.id === id) {
          item.quantity += 1; // Inkrementiraj količinu
        }
        return item;
      });

      this.cartData.total = this.getCartTotal();
    }

    this.cartDataObs$.next({ ...this.cartData });
    localStorage.setItem('cart', JSON.stringify(this.cartData));
  }

  clearCart(): void {
    this.cartData = {
      products: [],
      total: 0,
    };
    this.cartDataObs$.next({ ...this.cartData });
    localStorage.setItem('cart', JSON.stringify(this.cartData));
  }

  removeProduct(id: number): void {
    let updatedProducts = this.cartData.products.map(cartItem => {
      if (cartItem.product.id === id) {
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
  
    // Obavestite pretplatnike o promenama
    this.cartDataObs$.next({ ...this.cartData });
  
    // Sačuvajte ažuriranu korpu u localStorage
    localStorage.setItem('cart', JSON.stringify(this.cartData));

    // this._notification.create(
    //   'success',
    //   'Removed successfully',
    //   'The selected item was removed from the cart successfully'
    // );
  }

  updateCart(id: number, quantity: number): void {
    // copy array, find item index and update
    let updatedProducts = [...this.cartData.products];
    let productIndex = updatedProducts.findIndex((prod) => prod.product.id == id);

    updatedProducts[productIndex] = {
      ...updatedProducts[productIndex],
      quantity: quantity,
    };

    this.cartData.products = updatedProducts;
    this.cartData.total = this.getCartTotal();
    this.cartDataObs$.next({ ...this.cartData });
    localStorage.setItem('cart', JSON.stringify(this.cartData));
  }


  isProductInCart(id: number): boolean {
    return this.cartData.products.findIndex((prod) => prod.product.id === id) !== -1;
  }

  getCartTotal(): number {
    let totalSum = 0;
    this.cartData.products.forEach(
      (prod) => (totalSum += prod.product.price * prod.quantity)
    );

    return totalSum;
  }
}

