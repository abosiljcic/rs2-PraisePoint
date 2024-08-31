import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IAppState } from '../../../shared/app-state/app-state';
import { AppStateService } from '../../../shared/app-state/app-state.service';
import { UserFacadeService } from '../../domain/application-services/user-facade.service';
import { IUserDetails } from '../../domain/models/user-details';

@Component({
  selector: 'app-company-users',
  templateUrl: './company-users.component.html',
  styleUrl: './company-users.component.css'
})
export class CompanyUsersComponent implements OnInit {
  public appState$: Observable<IAppState>;
  public users: IUserDetails[] = [];
  companyId: string | undefined;

  constructor(private appStateService: AppStateService,private userService: UserFacadeService) {
    this.appState$ = this.appStateService.getAppState();
  }

  ngOnInit(): void {
    this.appState$.subscribe((state: IAppState) => {
      this.companyId = state.companyId
    });

    this.userService.getUsersByCompanyId(this.companyId).subscribe(
      (data: IUserDetails[]) => {
        this.users = data;
      },
      error => {
        console.error('Error fetching users:', error);
      }
    );
  }
}
