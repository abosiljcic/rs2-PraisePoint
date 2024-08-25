import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { ILoginRequest } from '../models/login-request';
import { ILoginResponse } from '../models/login-response';
import { map, Observable } from 'rxjs';
import { ILogoutRequest } from '../models/logout-request';
import { IRefreshTokenRequest } from '../models/refresh-token-request';
import { IRefreshTokenResponse } from '../models/refresh-token-response';
import { IRegisterRequest } from '../models/register-request';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private readonly url: string = 'http://localhost:8002/api/v1/Authentication';

  constructor(private httpClient: HttpClient) {}

  public login(request: ILoginRequest): Observable<ILoginResponse> {
    return this.httpClient.post<ILoginResponse>(`${this.url}/Login`, request);
  }

  public logout(request: ILogoutRequest): Observable<Object> {
    return this.httpClient.post(`${this.url}/Logout`, request);
  }

  public register(request: IRegisterRequest): Observable<HttpResponse<Object>> {
    return this.httpClient.post<HttpResponse<Object>>(`${this.url}/RegisterEmployee`, request);
  }

  public refreshToken(request: IRefreshTokenRequest): Observable<IRefreshTokenResponse> {
    return this.httpClient.post<IRefreshTokenResponse>(`${this.url}/Refresh`, request);
  }
}

