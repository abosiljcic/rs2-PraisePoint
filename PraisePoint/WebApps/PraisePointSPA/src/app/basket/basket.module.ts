import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BasketRoutingModule } from './basket-routing.module';
import { BasketComponent } from './basket.component';
import { CartComponent } from './components/cart/cart.component';
import { ProductItemComponent } from './components/product-item/product-item.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { CartItemComponent } from './components/cart-item/cart-item.component';
import { OrderComponent } from './components/order/order.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    BasketComponent,
    CartComponent,
    ProductListComponent,
    ProductItemComponent,
    CartItemComponent,
    OrderComponent
  ],
  imports: [
    CommonModule,
    BasketRoutingModule,
    ReactiveFormsModule
  ]
})
export class BasketModule { }
