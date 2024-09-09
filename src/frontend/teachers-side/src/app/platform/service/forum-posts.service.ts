import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPost } from '../models/post';
import { UserService } from 'src/app/authentication/service/user.service';

@Injectable({
  providedIn: 'root',
})
export class ForumPostsService {
  constructor(
    @Inject(HttpClient) private readonly httpClient: HttpClient,
    @Inject(UserService) private readonly userService: UserService
  ) {}

  public getPosts(subjectId: number): Observable<IPost[]> {
    const headers = this.buildRequestHeaders();
    return this.httpClient.get<IPost[]>(
      `https://localhost:7067/api/forums/${subjectId}/posts`,
      { headers }
    );
  }

  public addForumPost(forumPost: IPost): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    const creator = this.userService.getUser();
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/posts/add`,
      { ...forumPost, creator },
      { headers }
    );
  }

  public editForumPost(forumPost: IPost): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    const creator = this.userService.getUser();
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/posts/${forumPost.id}/edit`,
      { ...forumPost, creator },
      { headers }
    );
  }

  private buildRequestHeaders() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }
}
