import { Component, OnInit, Input } from '@angular/core';
import { Post } from '../../models/post.model';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../../services/post.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BehaviorSubject, Observable, map, switchMap, tap } from 'rxjs';
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

  public appState$: BehaviorSubject<IAppState>;
  usernameOfLoggedUser: string = '';
  imgUrl: string = 'https://mdbcdn.b-cdn.net/img/Photos/Avatars/img%20(10).webp';

  addCommentForm: FormGroup;
  isLiked: boolean = false;
  comment: String = "";
  isCommentClicked: boolean = false;
  canDelete = "False";
  companyId: string | undefined = '';

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
    const storedState = localStorage.getItem('appState');
    console.log('Stored state:', storedState);

    const initialState: IAppState = storedState ? JSON.parse(storedState) : {};
    this.appState$ = new BehaviorSubject<IAppState>(initialState);

    this.addCommentForm = this.formBuilder.group({
      comments: '',
    });

  }

  ngOnInit(): void {
    this.appState$.subscribe((state: IAppState) => {
      this.usernameOfLoggedUser = state.username ?? 'defaultUsername';
      //this.imgUrl = state.imageUrl;     kad pulujes otkomentarisi
      this.companyId = state.companyId;
      console.log("Ovo je user:", this.usernameOfLoggedUser);
    });
  }

  AddLike() {
    this.isLiked = !this.isLiked;
    console.log("lajk:" + this.isLiked);
    if (this.isLiked) {
      this.postService.addLike(this.usernameOfLoggedUser, this.post.id)
        .subscribe((result) => { });
    }
    else {
      //delete like
    }
  }

  openCommentForm() {
    this.isCommentClicked = !this.isCommentClicked;
  }

  onAddText(event: Event): void {
    const comment = (<HTMLInputElement>event.target).value;
    this.comment = comment;
  }

  submitForm(data: any) {
    this.addCommentForm.reset({
      comment: '',
    });

    this.postService
      .addComment(this.usernameOfLoggedUser, this.post.id, this.comment)
      .subscribe((post: Post) => { });

    this.isCommentClicked = false;

  }

  onCancelComment() {
    this.addCommentForm.reset({
      comment: '',
    });
  }

  //dodaj brisanje komentara
}
