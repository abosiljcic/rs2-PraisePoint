import { Component, OnInit } from '@angular/core';
import { IReward } from '../../models/reward';
import { RewardService } from '../../services/reward.service';

@Component({
  selector: 'app-reward',
  //standalone: true,
  //imports: [],
  templateUrl: './reward.component.html',
  styleUrl: './reward.component.css'
})
export class RewardComponent implements OnInit {

  reward!: IReward;

  constructor(private rewardService: RewardService) { }


  ngOnInit(): void {
    this.rewardService.rewardsDataObs$.subscribe((reward: IReward) => {
      this.reward = reward;
    });
  }

}
