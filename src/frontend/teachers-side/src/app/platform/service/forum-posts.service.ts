import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPost } from '../models/post';

@Injectable({
  providedIn: 'root'
})
export class ForumPostsService {

  constructor(@Inject(HttpClient) private readonly httpClient: HttpClient)
  { }

  public getPosts(subjectId: number): Observable<IPost[]> {
    const headers = this.buildRequestHeaders()
    return this.httpClient.get<IPost[]>(`https://localhost:7067/api/forums/${subjectId}/posts`, { headers });
  }

  private buildRequestHeaders() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }
}
