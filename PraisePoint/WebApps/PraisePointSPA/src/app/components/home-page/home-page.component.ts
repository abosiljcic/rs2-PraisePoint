import { Component, OnInit } from '@angular/core';
import { Post } from '../../models/post.model';
import { BehaviorSubject, Observable, Subscription, startWith, switchMap } from 'rxjs';
import { PostService } from '../../services/post.service';
import { PostProfileComponent } from '../post-profile/post-profile.component';
import { CommonModule } from '@angular/common';
import { AddPostComponent } from '../add-post/add-post.component';
import { IAppState } from '../../shared/app-state/app-state';
import { AppStateService } from '../../shared/app-state/app-state.service';
import { UserProfileComponent } from '../../user/feature-user-info/user-profile/user-profile.component';
import moment from 'moment';


@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [CommonModule, PostProfileComponent, AddPostComponent, UserProfileComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {
  posts: Post[] = [];
  public appState$: BehaviorSubject<IAppState>;
  companyId: string | undefined;
  createdDate: string | undefined;


  sub: Subscription = new Subscription();

  constructor(
    private postsService: PostService,
    private appStateService: AppStateService
  ) {
    const storedState = localStorage.getItem('appState');

    const initialState: IAppState = storedState ? JSON.parse(storedState) : {};
    this.appState$ = new BehaviorSubject<IAppState>(initialState);
  }

  ngOnInit(): void {
    // Pretplaćujemo se na appState$ kako bismo pratili promene u stanju
    this.appState$.subscribe((state: IAppState) => {
      this.companyId = state.companyId //?? '18809c4c-f5d3-421a-9a4e-0ac08b247352';
      console.log("Ovo je user:", this.companyId);
    });
    
    if (this.companyId) {
      this.postsService.RefreshRequired
        .pipe(
          startWith(0), // Pokreće prvi put kada se komponenta inicijalizuje
          switchMap(() => this.postsService.getPosts(this.companyId!))
        )
        .subscribe((posts) => {
          this.posts = posts
            .map(post => {
              //console.log(post.createdDate);
              post.createdDate = moment(post.createdDate).fromNow();
              return post;
            });
        });
    }
  }

}
