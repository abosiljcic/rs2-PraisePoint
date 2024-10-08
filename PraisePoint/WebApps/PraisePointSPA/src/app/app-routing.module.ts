import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { NavigationComponent } from './navigation/navigation.component';

const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'home-page', component: HomePageComponent },
  { path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule) },
  { path: 'basket', loadChildren: () => import('./basket/basket.module').then(m => m.BasketModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


