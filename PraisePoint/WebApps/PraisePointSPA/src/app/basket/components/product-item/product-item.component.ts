import { Component, OnInit, Input } from '@angular/core';
import { IProduct } from '../../models/product'
import { CartService } from '../../services/cart.service';
import { CartComponent } from '../cart/cart.component';
import { Observable } from 'rxjs';
import { IAppState } from '../../../shared/app-state/app-state';
import { AppStateService } from '../../../shared/app-state/app-state.service';

@Component({
  selector: 'app-product-item',
  //standalone: true,
  //imports: [],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent implements OnInit{

  @Input() product!: IProduct;

  public appState$: Observable<IAppState>;

  private username: string | undefined;

  constructor(private cartService: CartService, private appStateService: AppStateService) {
    this.appState$ = this.appStateService.getAppState();
   }

  addToCart(): void {
    // Pozivamo metodu iz CartService da dodamo proizvod u korpu
    this.cartService.addProduct({
      productId: this.product.productId,
      price: this.product.price,
      pictureUrl: this.product.pictureUrl,
      productName: this.product.productName
    }, this.username);
  }

  ngOnInit(): void {
    this.appState$.subscribe((state: IAppState) => {
      this.username = state.username
      console.log("Product Item Comp: Ovo je user:", this.username);
    });
  }
}
