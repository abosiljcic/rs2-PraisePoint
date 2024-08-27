import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserDetails } from '../models/user-details';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpClient: HttpClient) {}

  public getUserDetails(username: string): Observable<IUserDetails> {
    return this.httpClient.get<IUserDetails>(`http://localhost:8002/api/v1/User/${username}`);
  }
}