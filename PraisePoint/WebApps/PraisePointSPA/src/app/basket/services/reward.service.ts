import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { IReward } from '../models/reward';
import { of, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RewardService {

  private reward: Observable<IReward> = new Observable<IReward>();

  private readonly rewardUrl = 'http://localhost:8003';

  /*private reward = 
    {username: "KatMilo", receivedPoints: 234, budget: 123, 
      companyId: "123asd"
    };*/

  constructor(private http: HttpClient) { } 

  getRewards(username: string | undefined): Observable<IReward> {
    this.reward = this.http.get<IReward>(this.rewardUrl + "/users/" + username);
    return this.reward;
  }

}
