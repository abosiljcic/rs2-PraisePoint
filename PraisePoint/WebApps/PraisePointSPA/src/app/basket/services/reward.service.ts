import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { IReward } from '../models/reward';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RewardService {

  private reward = 
    {username: "KatMilo", receivedPoints: 234, budget: 123, 
      companyId: "123asd"
    };

  constructor() { }

  getRewards() {
    return this.reward;
  }

  get rewardsDataObs$(): Observable<IReward> {
    return of(this.reward); 
  }
}
