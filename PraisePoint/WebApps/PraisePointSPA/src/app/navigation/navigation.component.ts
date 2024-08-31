import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faShop, faHome, faCartShopping, faUser } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { IAppState } from '../shared/app-state/app-state';
import { AppStateService } from '../shared/app-state/app-state.service';
import { RouterModule } from '@angular/router';
import { Role } from '../shared/app-state/role';


@Component({
  selector: 'app-navigation',
  standalone: true,
  imports : [FontAwesomeModule, RouterModule, CommonModule],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.css'
})
export class NavigationComponent implements OnInit {
  faShop = faShop;
  faCart = faCartShopping;
  faHome = faHome;
  faUser = faUser;

  public appState$: Observable<IAppState>;
  public appState: IAppState | null = null;

  constructor(private appStateService: AppStateService) {
    this.appState$ = this.appStateService.getAppState();
  }

  ngOnInit(): void {
    this.appState$.subscribe((state: IAppState) => {
      this.appState = state; 
    });
  }

  public userLoggedIn(): boolean {
    return this.appState ? !this.userLoggedOut(this.appState) : false;
  }

  public userLoggedOut(appState: IAppState): boolean {
    return appState.isEmpty();
  }

  public hasAdminRole(appState: IAppState): boolean {
    return appState.getRole() === Role.Admin;
  }

}
