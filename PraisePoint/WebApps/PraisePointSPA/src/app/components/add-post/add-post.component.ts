import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PostService } from '../../services/post.service';
import { Post } from '../../models/post.model';
import { IAppState } from '../../shared/app-state/app-state';
import { BehaviorSubject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { AppStateService } from '../../shared/app-state/app-state.service';

@Component({
  selector: 'app-add-post',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-post.component.html',
  styleUrl: './add-post.component.css'
})
export class AddPostComponent implements OnInit {
  addPostForm: FormGroup;

  public appState$: BehaviorSubject<IAppState>;
  loggedUsername: string | undefined = ""; //izvuci iz podataka
  companyId: string | undefined = "18809c4c-f5d3-421a-9a4e-0ac08b247352";
  budget: number = 300; //ovo treba da izvucem iz inf o useru kaca to ubacuje negde

  constructor(
    private postService: PostService,
    private formBuilder: FormBuilder,
    private appStateService: AppStateService,
  ) {
    const storedState = localStorage.getItem('appState');
    const initialState: IAppState = storedState ? JSON.parse(storedState) : {};
    this.appState$ = new BehaviorSubject<IAppState>(initialState);

    this.addPostForm = this.formBuilder.group({
      receiverUsername: ['', [Validators.required, Validators.minLength(5)]], //ovde moze da se doda validator koji poziva bek i proverava da li taj user postoji u bazi
      points: ['', [Validators.required, Validators.min(0), Validators.max(this.budget)]],
      description: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.appState$.subscribe((state: IAppState) => {
      this.loggedUsername = state.username ?? 'defaultUsername';
      //this.imgUrl = state.imageUrl;     kad pulujes otkomentarisi
      this.companyId = state.companyId;
      console.log("kompani aj di:" + state.companyId);
      console.log("jusernejm:" + state.username);

      
    });
  }

  public onCancelPost() {
    this.addPostForm.reset({
      receiverUsername: '',
      points: '',
      description: '',
    });
  }

  public onSubmitForm(data: any) {
    if (!this.addPostForm.valid) {
      window.alert('Form is not valid!');
      return;
    }

    const body = {
      senderUsername: this.loggedUsername,
      receiverUsername: data.receiverUsername,
      companyId: this.companyId,
      points: data.points,
      description: data.description
    };

    console.log(body);

    this.postService.addPost(body).subscribe((post: Post) => {
      console.log("usao");
      console.log(post);
      window.alert(`Adding post is successfully!`);
    });

    this.addPostForm.reset({
      receiverUsername: '',
      points: '',
      description: '',
    });
  }

}
