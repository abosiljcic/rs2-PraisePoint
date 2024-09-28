import { Component, OnInit, Input} from '@angular/core';
import { ICartItem } from '../../models/cart-item'; 
import { CartService } from '../../services/cart.service';
import { Observable } from 'rxjs';
import { IAppState } from '../../../shared/app-state/app-state';
import { AppStateService } from '../../../shared/app-state/app-state.service';

@Component({
  selector: 'app-cart-item',
  // standalone: true,
  // imports: [],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent implements OnInit{

  @Input() cartitem!: ICartItem;

  public appState$: Observable<IAppState>;

  private username: string | undefined;

  constructor(private cartService: CartService, private appStateService: AppStateService) { 
    this.appState$ = this.appStateService.getAppState();
  }

  ngOnInit(): void {
    this.appState$.subscribe((state: IAppState) => {
      this.username = state.username
      console.log("Cart item Comp: Ovo je user:", this.username);
    });
  }

  addToCart() : void {
    this.cartService.removeProduct(this.cartitem.product.id, this.username);
  }

  get quantity(): number {
    return this.cartitem.quantity;
  }

  incrementQuantity(): void {
    this.cartitem.quantity++;
    this.cartService.updateCart(this.cartitem.product.id, this.cartitem.quantity);
  }

  decrementQuantity(): void {
    if (this.cartitem.quantity > 1) {
      this.cartitem.quantity--;
      this.cartService.updateCart(this.cartitem.product.id, this.cartitem.quantity);
    } else if (this.cartitem.quantity === 1) {
      this.cartService.removeProduct(this.cartitem.product.id, this.username);
    }
  }
}
