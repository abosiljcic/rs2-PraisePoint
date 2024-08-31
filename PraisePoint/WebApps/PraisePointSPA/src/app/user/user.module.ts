import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginFormComponent } from './feature-authentication/login-form/login-form/login-form.component';
import { UserProfileComponent } from './feature-user-info/user-profile/user-profile.component';
import { LogoutComponent } from './feature-authentication/logout/logout/logout.component';
import { RegisterFormComponent } from './feature-authentication/register-form/register-form.component';


@NgModule({
  declarations: [
    UserComponent,
    LoginFormComponent,
   // UserProfileComponent,
    LogoutComponent,
    RegisterFormComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    ReactiveFormsModule
  ]
})
export class UserModule { }
