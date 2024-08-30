import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PostService } from '../../services/post.service';
import { Post } from '../../models/post.model';

@Component({
  selector: 'app-add-post',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './add-post.component.html',
  styleUrl: './add-post.component.css'
})
export class AddPostComponent implements OnInit {
  addPostForm: FormGroup;
  loggedUsername: string = "nikolina"; //izvuci iz podataka
  companyId: string = "18809c4c-f5d3-421a-9a4e-0ac08b247352";
  budget: number = 300; //ovo treba da izvucem iz inf o useru kaca to ubacuje negde

  constructor(
    private postService: PostService,
    private formBuilder: FormBuilder,
  ) {
    this.addPostForm = this.formBuilder.group({
      receiverUsername: ['', [Validators.required, Validators.minLength(5)]], //ovde moze da se doda validator koji poziva bek i proverava da li taj user postoji u bazi
      points: ['', [Validators.required, Validators.min(0), Validators.max(this.budget)]],
      description: ['', Validators.required]
    });
  }

  ngOnInit(): void {
        
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
      //refresuje se feed
    });

    this.addPostForm.reset({
      receiverUsername: '',
      points: '',
      description: '',
    });
  }

}
