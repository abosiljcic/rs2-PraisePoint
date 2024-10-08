import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, switchMap, take } from 'rxjs';
import { IAppState } from '../../../shared/app-state/app-state';
import { AppStateService } from '../../../shared/app-state/app-state.service';
import { JwtPayloadKeys } from '../../../shared/jwt/jwt-payload-keys';
import { JwtService } from '../../../shared/jwt/jwt.service';
import { AuthenticationService } from '../infrastructure/authentication.service';
import { ILoginRequest } from '../models/login-request';
import { ILoginResponse } from '../models/login-response';
import { IUserDetails } from '../models/user-details';
import { UserFacadeService } from './user-facade.service';
import { ILogoutRequest } from '../models/logout-request';
import { IRefreshTokenRequest } from '../models/refresh-token-request';
import { IRefreshTokenResponse } from '../models/refresh-token-response';
import { IRegisterRequest } from '../models/register-request';
import { HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationFacadeService {
  constructor(
    private authenticationService: AuthenticationService,
    private appStateService: AppStateService,
    private jwtService: JwtService,
    private userService: UserFacadeService
  ) {}

  public login(username: string, password: string): Observable<boolean> {
    const request: ILoginRequest = { username, password };

    return this.authenticationService.login(request).pipe(
      switchMap((loginResponse: ILoginResponse) => {
        this.appStateService.setAccessToken(loginResponse.accessToken);
        this.appStateService.setRefreshToken(loginResponse.refreshToken);

        const payload = this.jwtService.parsePayload(loginResponse.accessToken);
        this.appStateService.setUsername(payload[JwtPayloadKeys.Username]);
        this.appStateService.setEmail(payload[JwtPayloadKeys.Email]);
        this.appStateService.setRoles(payload[JwtPayloadKeys.Role]);

        return this.userService.getUserDetails(payload[JwtPayloadKeys.Username]);
      }),
      map((userDetails: IUserDetails) => {
        this.appStateService.setFirstName(userDetails.firstName);
        this.appStateService.setLastName(userDetails.lastName);
        this.appStateService.setUserId(userDetails.id);
        this.appStateService.setPhoneNumber(userDetails.phoneNumber);
        this.appStateService.setImageUrl(userDetails.imageUrl);
        this.appStateService.setCompanyId(userDetails.companyId);
        this.appStateService.setPointsNumber(userDetails.pointsNumber);
        //this.appStateService.setUsername(userDetails.username);

        return true;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();
        return of(false);
      })
    );
  }

  public logout(): Observable<boolean> {
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: ILogoutRequest = { userName: appState.username as string, refreshToken: appState.refreshToken as string };
        return request;
      }),
      switchMap((request: ILogoutRequest) => this.authenticationService.logout(request)),
      map(() => {
        this.appStateService.clearAppState();
        return true;
      }),
      catchError((err) => {
        console.error(err);
        return of(false);
      })
    );
  }

  public register(firstName: string, lastName: string, userName: string, password: string, imageUrl: string, email: string, 
                  phoneNumber: string, companyId: string
  ): Observable<boolean> {
    const request: IRegisterRequest = { firstName, lastName, userName, password, imageUrl, email,  phoneNumber, companyId };

    return this.authenticationService.register(request).pipe(
      map((response: HttpResponse<Object> | null) => {
        return true;
    }),
      catchError((error) => {
        console.error('Registration failed', error);
        this.appStateService.clearAppState();
        return of(false);
      })
    );
  
  }

  public refreshToken(): Observable<string | null> {
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: IRefreshTokenRequest = { userName: appState.username as string, refreshToken: appState.refreshToken as string };
        return request;
      }),
      switchMap((request: IRefreshTokenRequest) => this.authenticationService.refreshToken(request)),
      map((response: IRefreshTokenResponse) => {
        this.appStateService.setAccessToken(response.accessToken);
        this.appStateService.setRefreshToken(response.refreshToken);

        return response.accessToken;
      }),
      catchError((err) => {
        console.log(err);
        this.appStateService.clearAppState();
        return of(null);
      })
    );
  }
}