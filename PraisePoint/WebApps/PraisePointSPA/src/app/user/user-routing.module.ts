import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user.component';
import { LoginFormComponent } from './feature-authentication/login-form/login-form/login-form.component';
import { UserProfileComponent } from './feature-user-info/user-profile/user-profile.component';
import { LogoutComponent } from './feature-authentication/logout/logout/logout.component';
import { NotAuthenticatedGuard } from '../shared/guards/not-authenticated.guard';
import { RegisterFormComponent } from './feature-authentication/register-form/register-form.component';

// Podrazumeva se prefix /user

const routes: Routes = [
  { path: '', component: UserComponent, children: [{ path: 'login', component: LoginFormComponent }] },
  { path: 'profile', component: UserProfileComponent, canActivate: [NotAuthenticatedGuard] },
  { path: 'logout', component: LogoutComponent },
  { path: 'register', component: RegisterFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }