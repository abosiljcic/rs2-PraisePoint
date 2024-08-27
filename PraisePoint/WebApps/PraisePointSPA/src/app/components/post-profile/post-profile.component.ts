import { Component, OnInit, Input } from '@angular/core';
import { Post } from '../../models/post.model';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../../services/post.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Observable, switchMap, tap } from 'rxjs';
import { IAppState } from '../../shared/app-state/app-state';
import { AppStateService } from '../../shared/app-state/app-state.service';

@Component({
  selector: 'app-post-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './post-profile.component.html',
  styleUrl: './post-profile.component.css'
})
export class PostProfileComponent implements OnInit {
  @Input('postData')
  public post!: Post;
  public appState$: Observable<IAppState>;
  usernameOfLoggedUser: string | undefined;
  addCommentForm: FormGroup;
  isLiked: boolean = false;
  comment: String = "";

  canDelete = "True";

  //id: string;

  constructor(
    private appStateService: AppStateService,
    //private activatedRoute: ActivatedRoute,
    private postService: PostService,
    private formBuilder: FormBuilder,
  ) {
    //this.id = this.activatedRoute.snapshot.paramMap.get('id')!;
    //this.postService.getPostById(this.id).subscribe((post) => {
      //this.post = post as Post;
    //});
    this.appState$ = this.appStateService.getAppState();

    this.addCommentForm = this.formBuilder.group({
      comments: '',
    });

  }

  ngOnInit(): void {
    this.appState$.subscribe((state: IAppState) => {
      this.usernameOfLoggedUser = state.username;
      //console.log("ovo je user:" + this.usernameOfLoggedUser);
    });    
  }

  //ovde cu valjda da imam za dodavanje komentara, lajkova

  AddLike() {
    this.isLiked = !this.isLiked;
    console.log("lajk:" + this.isLiked);
    this.usernameOfLoggedUser = "GLUPIUSER1";
    if (this.isLiked) {
      this.postService.addLike(this.usernameOfLoggedUser, this.post.id)
        .subscribe((result) => { });
    }
    else {
      //delete like
    }
  }

  onAddText(event: Event): void {
    const comment = (<HTMLInputElement>event.target).value;
    this.comment = comment;
  }

  submitForm(data: any) {
    this.addCommentForm.reset({
      comment: '',
    });
    this.usernameOfLoggedUser = "GLUPIUSER"; //ovo je dok ne dohvatim lepo ulogovanog usera

    this.postService
      .addComment(this.usernameOfLoggedUser!, this.post.id, this.comment)
      .subscribe((post: Post) => { });
  }

}
