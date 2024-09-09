import { Injectable } from '@angular/core';
import { Post } from '../models/post.model';
import { Observable, Subject, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class PostService {
  private posts: Observable<Post[]> = new Observable<Post[]>();

  private readonly postsUrl = 'http://localhost:8004';

  private refreshRequired = new Subject<void>();
  private refreshCommentsRequired = new Subject<void>();
  get RefreshRequired() {
    return this.refreshRequired;
  }
  get RefreshCommentsRequired() {
    return this.refreshCommentsRequired;
  }

  constructor(private http: HttpClient) {
    //this.getPosts("18809c4c-f5d3-421a-9a4e-0ac08b247352");
  }

  refreshPosts(companyId: string): Observable<Post[]> {
    this.posts = this.http.get<Post[]>(this.postsUrl + "/company/" + companyId);
    return this.posts;
  }


  getPosts(companyId: string): Observable<Post[]> {
    this.posts = this.http.get<Post[]>(this.postsUrl + "/company/" + companyId);
    //console.log(this.posts.pipe(map(posts => posts.map(post => `From: ${post.senderUsername}, To: ${post.receiverUsername}, Message: ${post.description}`))));
    return this.posts;
  }

  getPostsBySenderUsername(senderUsername: string): Observable<Post[]> {
    this.posts = this.http.get<Post[]>(this.postsUrl + "/sender-username/" + senderUsername);
    //console.log(this.posts.pipe(map(posts => posts.map(post => `From: ${post.senderUsername}, To: ${post.receiverUsername}, Message: ${post.description}`))));
    return this.posts;
  }

  getPostsByReceiverUsername(receiverUsername: string): Observable<Post[]> {
    this.posts = this.http.get<Post[]>(this.postsUrl + "/receiver-username/" + receiverUsername);
    //console.log(this.posts.pipe(map(posts => posts.map(post => `From: ${post.senderUsername}, To: ${post.receiverUsername}, Message: ${post.description}`))));
    return this.posts;
  }

  getPostById(id: string): Observable<Post> {
    return this.http.get<Post>(this.postsUrl + "/id/" + id);
  }

  addPost(data: any): Observable<Post> {
    console.log("u servisu sam zovem bek")
    return this.http.post<Post>(this.postsUrl + "/api/v1/Post", data)
      .pipe(
        tap(() => {
          this.refreshRequired.next(); // Emitovanje događaja kada se post doda
        })
      );
  }

  addLike(
    username: String,
    postId: String,
  ): Observable<Post> {
    const data = { username, postId };

    return this.http.post<Post>(this.postsUrl + "/likes", data)
      .pipe(
        tap(() => {
          this.refreshRequired.next(); // Emitovanje događaja kada se doda komentar na post
        })
      );
  }

  addComment(
    username: String,
    postId: String,
    text: String
  ): Observable<Post> {
    const data = { username, postId, text };

    return this.http.post<Post>(this.postsUrl + "/comments", data)
      .pipe(
        tap(() => {
          this.refreshRequired.next(); // Emitovanje događaja kada se doda komentar na post
        })
      );
  }


}
