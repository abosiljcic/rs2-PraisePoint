import { Component, OnInit } from '@angular/core';
import { Post } from '../../models/post.model';
import { Subscription } from 'rxjs';
import { PostService } from '../../services/post.service';
import { PostProfileComponent } from '../post-profile/post-profile.component';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [CommonModule, PostProfileComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {
  posts: Post[] = [];
  companyId = "18809c4c-f5d3-421a-9a4e-0ac08b247352" //ovo se kasnije sa user komponente dovlaci


  sub: Subscription = new Subscription();

  constructor(private postsService: PostService) {
    
  }

  ngOnInit(): void {
    this.postsService.getPosts(this.companyId).subscribe((posts) => {
      this.posts = posts as Post[];      
    });
  }

}
