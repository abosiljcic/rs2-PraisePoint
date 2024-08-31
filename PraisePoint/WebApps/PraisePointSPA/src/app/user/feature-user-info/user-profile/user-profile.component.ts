import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IAppState } from '../../../shared/app-state/app-state';
import { AppStateService } from '../../../shared/app-state/app-state.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
})
export class UserProfileComponent implements OnInit {
  public appState$: Observable<IAppState>;

  constructor(private appStateService: AppStateService) {
    this.appState$ = this.appStateService.getAppState();
  }

  ngOnInit(): void {}
}