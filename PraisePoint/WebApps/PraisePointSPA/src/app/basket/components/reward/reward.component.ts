import { Component, OnInit } from '@angular/core';
import { IReward } from '../../models/reward';
import { RewardService } from '../../services/reward.service';
import { Observable, map, switchMap } from 'rxjs';
import { IAppState } from '../../../shared/app-state/app-state';
import { AppStateService } from '../../../shared/app-state/app-state.service';

@Component({
  selector: 'app-reward',
  //standalone: true,
  //imports: [],
  templateUrl: './reward.component.html',
  styleUrl: './reward.component.css'
})
export class RewardComponent implements OnInit {

  reward!: IReward;
  public appState$: Observable<IAppState>;
  username: string | undefined;

  constructor(private rewardService: RewardService, private appStateService: AppStateService) 
  { 
    this.appState$ = this.appStateService.getAppState();
  }


  ngOnInit(): void {
    this.appState$
    .pipe(
      map((state: IAppState) => state.username),
      switchMap((username: string | undefined) => {
        console.log("Reward: Ovo je user:", username);
        return this.rewardService.getRewards(username);
      })
    )
    .subscribe({
      next: (reward: IReward) => {
        this.reward = reward;  
        console.log("Reward in component:", reward);
      },
      error: (error) => {
        console.error("Error fetching reward in component:", error);
      }
    });
  } 
}
