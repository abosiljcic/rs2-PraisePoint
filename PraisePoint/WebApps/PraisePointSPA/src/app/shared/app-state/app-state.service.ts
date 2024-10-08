import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LocalStorageKeys } from '../local-storage/local-storage-keys';
import { LocalStorageService } from '../local-storage/local-storage.service';
import { AppState, IAppState } from './app-state';
import { Role } from './role';

@Injectable({
  providedIn: 'root',
})
export class AppStateService {
  private appState: IAppState = new AppState();
  private appStateSubject: BehaviorSubject<IAppState> = new BehaviorSubject<IAppState>(this.appState);
  private appStateObservable: Observable<IAppState> = this.appStateSubject.asObservable();

  constructor(private localStorageService: LocalStorageService) {
    this.restoreFromLocalStorage();
  }

  public getAppState(): Observable<IAppState> {
    return this.appStateObservable;
  }

  public setAccessToken(accessToken: string): void {
    this.appState = this.appState.clone();
    this.appState.accessToken = accessToken;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setRefreshToken(refreshToken: string): void {
    this.appState = this.appState.clone();
    this.appState.refreshToken = refreshToken;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setUsername(username: string): void {
    this.appState = this.appState.clone();
    this.appState.username = username;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setPointsNumber(pointsNumber: number): void {
    this.appState = this.appState.clone();
    this.appState.pointsNumber = pointsNumber;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setEmail(email: string): void {
    this.appState = this.appState.clone();
    this.appState.email = email;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setRoles(roles: Role | Role[]): void {
    this.appState = this.appState.clone();
    this.appState.roles = roles;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setFirstName(firstName: string): void {
    this.appState = this.appState.clone();
    this.appState.firstName = firstName;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setLastName(lastName: string): void {
    this.appState = this.appState.clone();
    this.appState.lastName = lastName;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setPhoneNumber(phoneNumber: string): void {
    this.appState = this.appState.clone();
    this.appState.phoneNumber = phoneNumber;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setImageUrl(imageUrl: string): void {
    this.appState = this.appState.clone();
    this.appState.imageUrl = imageUrl;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setCompanyId(companyId: string): void {
    this.appState = this.appState.clone();
    this.appState.companyId = companyId;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public setUserId(userId: string): void {
    this.appState = this.appState.clone();
    this.appState.userId = userId;
    this.appStateSubject.next(this.appState);
    this.localStorageService.set(LocalStorageKeys.AppState, this.appState);
  }

  public clearAppState(): void {
    this.localStorageService.clear(LocalStorageKeys.AppState);
    this.appState = new AppState();
    this.appStateSubject.next(this.appState);
  }

  private restoreFromLocalStorage(): void {
    const appState: IAppState | null = this.localStorageService.get(LocalStorageKeys.AppState);
    if (appState !== null) {
      this.appState = new AppState(
        appState.accessToken,
        appState.refreshToken,
        appState.username,
        appState.email,
        appState.roles,
        appState.firstName,
        appState.lastName,
        appState.userId
      );
      this.appStateSubject.next(this.appState);
    }
  }
}