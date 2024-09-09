import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './basket.component';
import { CartComponent } from './components/cart/cart.component';
import {ProductListComponent} from './components/product-list/product-list.component'
import { OrderComponent } from './components/order/order.component';
import { NotAuthenticatedGuard } from '../shared/guards/not-authenticated.guard';

const routes: Routes = [{ path: '', component: BasketComponent , canActivate: [NotAuthenticatedGuard],
                            children: [{ path: '', component: ProductListComponent, canActivate: [NotAuthenticatedGuard] }]},
                        { path: 'cart', component: CartComponent, canActivate: [NotAuthenticatedGuard] },
                        { path: '', component: BasketComponent, canActivate: [NotAuthenticatedGuard] },
                        {path: 'order', component: OrderComponent, canActivate: [NotAuthenticatedGuard]}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasketRoutingModule { }
