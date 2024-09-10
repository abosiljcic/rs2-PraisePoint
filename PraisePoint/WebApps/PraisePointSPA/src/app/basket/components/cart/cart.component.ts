import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { ICart } from '../../models/cart';
import { BehaviorSubject, Observable } from 'rxjs';
import { IProduct } from '../../models/product';
import { AppStateService } from '../../../shared/app-state/app-state.service';
import { IAppState } from '../../../shared/app-state/app-state';
import { ICartItem } from '../../models/cart-item';

@Component({
  selector: 'app-cart',
  //standalone: true,
  //imports: [],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  
  cartDataObs$;
  public appState$: Observable<IAppState>;

  private username: string | undefined;
  cartData: ICart;

  constructor(private cartService: CartService, private appStateService: AppStateService) { 
    this.appState$ = this.appStateService.getAppState();
    this.cartData = { 
      username: this.username,  // Correct syntax here
      products: [], 
      total: 0 
    };
    
  this.cartDataObs$ = new BehaviorSubject<ICart>(this.cartData);
  }

  ngOnInit(): void {
    this.cartService.cartDataObs$.subscribe((data: ICart) => {
      this.cartData = data;
    });

    this.appState$.subscribe((state: IAppState) => {
      this.username = state.username
      console.log("Cart Comp: Ovo je user:", this.username);
    });
  }

  clearCart(): void {
    this.cartData = {
      username: this.username,
      products: [],
      total: 0,
    };
    this.cartDataObs$.next({ ...this.cartData });
    localStorage.setItem('cart', JSON.stringify(this.cartData));

    // call endpoint
    this.cartService.deleteCart(this.username).subscribe({
      next: (response) => {
        console.log('Cart successfully updated on backend:', response);
      },
      error: (err) => {
        console.error('Error updating cart on backend:', err);
      }
    });
  }
}
