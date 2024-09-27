import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faShop, faHome, faCartShopping, faUser } from '@fortawesome/free-solid-svg-icons';
import { IAppState } from '../shared/app-state/app-state';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { AppStateService } from '../shared/app-state/app-state.service';
import { AuthenticationFacadeService } from '../user/domain/application-services/authentication-facade.service.ts.service';
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

  public appState$: BehaviorSubject<IAppState>;
  public appState2$: Observable<IAppState>;
  public appState: IAppState | null = null;
  activeSubscriptions: Subscription[] = [];
  private loggedInSubject = new BehaviorSubject<boolean>(false);
  isLoggedIn$: Observable<boolean> = this.loggedInSubject.asObservable();
  isLoggedIn: boolean = false;

  constructor(private appStateService: AppStateService, private authenticationService: AuthenticationFacadeService) {
    const storedState = localStorage.getItem('appState');

    const initialState: IAppState = storedState ? JSON.parse(storedState) : {};
    this.appState$ = new BehaviorSubject<IAppState>(initialState);
    this.appState2$ = this.appStateService.getAppState();
  }

  ngOnInit(): void {
    const sub = this.appState$.subscribe((state: IAppState) => {
      this.appState = state;
      this.loggedInSubject.next(!state.isEmpty());
      this.checkLoginStatus();
    });
    const sub2 = this.appState2$.subscribe((state: IAppState) => {
      console.log("Updating app state.");
      this.appState = state;
      this.loggedInSubject.next(!state.isEmpty());
      this.checkLoginStatus();
    });
    const sub3 = this.isLoggedIn$.subscribe(loggedIn => {
      this.isLoggedIn = loggedIn;
    });
    this.activeSubscriptions.push(sub3);
    this.activeSubscriptions.push(sub2);
    this.activeSubscriptions.push(sub);
  }

  ngOnDestroy() {
    this.activeSubscriptions.forEach((sub) => sub.unsubscribe);
  }

  checkLoginStatus() {
    const loggedIn = !!localStorage.getItem('appState');
    this.loggedInSubject.next(loggedIn);
    return this.isLoggedIn;
  }

  public logout() {
    this.loggedInSubject.next(false);
    this.authenticationService.logout();
    localStorage.removeItem('appState');
  }

  public userLoggedIn(): boolean {
    return this.appState ? true : false;
  }

  public hasAdminRole(): boolean {
    this.checkLoginStatus();
    if (typeof this.appState?.roles === 'string') {
      return this.appState.roles === Role.Admin;
    }
    return this.appState?.roles?.find((registeredRole: Role) => registeredRole === Role.Admin) !== undefined;
  }

}
