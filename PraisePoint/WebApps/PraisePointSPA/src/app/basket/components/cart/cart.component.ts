import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { ICart } from '../../models/cart';

@Component({
  selector: 'app-cart',
  //standalone: true,
  //imports: [],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  
  cartData: ICart = { products: [], total: 0 };

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.cartService.cartDataObs$.subscribe((data: ICart) => {
      this.cartData = data;
    });
  }

  clearCart(): void {
    this.cartService.clearCart();
    
  }


}
