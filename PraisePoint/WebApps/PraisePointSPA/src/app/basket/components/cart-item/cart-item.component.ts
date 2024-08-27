import { Component, OnInit, Input} from '@angular/core';
import { ICartItem } from '../../models/cart-item'; 
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-cart-item',
  // standalone: true,
  // imports: [],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent implements OnInit{

  @Input() cartitem!: ICartItem;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
  }

  addToCart() : void {
    this.cartService.removeProduct(this.cartitem.product.id);
  }

  get quantity(): number {
    return this.cartitem.quantity;
  }

  incrementQuantity(): void {
    this.cartitem.quantity++;
    this.cartService.updateCart(this.cartitem.product.id, this.cartitem.quantity );
  }

  decrementQuantity(): void {
    if (this.cartitem.quantity > 1) {
      this.cartitem.quantity--;
      this.cartService.updateCart(this.cartitem.product.id, this.cartitem.quantity );
    } else if (this.cartitem.quantity === 1) {
      this.cartService.removeProduct(this.cartitem.product.id);
    }
  }
}
