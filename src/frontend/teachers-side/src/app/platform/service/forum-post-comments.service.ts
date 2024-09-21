import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/authentication/service/user.service';
import { IComment } from '../models/comment';

@Injectable({
  providedIn: 'root',
})
export class ForumPostCommentsService {
  constructor(
    @Inject(HttpClient) private readonly httpClient: HttpClient,
    @Inject(UserService) private readonly userService: UserService
  ) {}

  public getPostComments(postId: number): Observable<IComment[]> {
    const headers = this.buildRequestHeaders();
    return this.httpClient.get<IComment[]>(
      `https://localhost:7067/api/comments?postId=${postId}`,
      { headers }
    );
  }

  public addPostComment(comment: IComment): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    const creator = this.userService.getUser();
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/comments/add`,
      { ...comment, creator },
      { headers }
    );
  }

  public editPostComment(comment: IComment) {
    const headers = this.buildRequestHeaders();
    const creator = this.userService.getUser();
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/comments/${comment.id}/edit`,
      { ...comment, creator },
      { headers }
    );
  }

  public deletePostComment(commentId: number) {
    const headers = this.buildRequestHeaders();
    return this.httpClient.delete<boolean>(
      `https://localhost:7067/api/comments/${commentId}`,
      { headers }
    );
  }

  private buildRequestHeaders() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }
}
