import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AuthenticationInterceptor } from './shared/interceptors/authentication.interceptor.ts.interceptor';
import { RouterModule } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { PostProfileComponent } from './components/post-profile/post-profile.component';
import { NavigationComponent } from "./navigation/navigation.component";
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { AddPostComponent } from './components/add-post/add-post.component';
import { CommonModule } from '@angular/common';


@NgModule({
  declarations: [
    AppComponent,
    //PostProfileComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    HomePageComponent,
    PostProfileComponent,
    FontAwesomeModule,
    NavigationComponent,
    AddPostComponent,
],
  
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
