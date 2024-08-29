import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './basket.component';
import { CartComponent } from './components/cart/cart.component';
import {ProductListComponent} from './components/product-list/product-list.component'
import { OrderComponent } from './components/order/order.component';

const routes: Routes = [{ path: '', component: BasketComponent , 
                            children: [{ path: '', component: ProductListComponent }]},
                        { path: 'cart', component: CartComponent },
                        { path: '', component: BasketComponent },
                        {path: 'order', component: OrderComponent}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasketRoutingModule { }
