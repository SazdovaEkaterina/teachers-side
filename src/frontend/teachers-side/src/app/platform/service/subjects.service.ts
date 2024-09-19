import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ISubject } from '../models/subject';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubjectsService {

  constructor(@Inject(HttpClient) private readonly httpClient: HttpClient)
  { }

  public getSubjects(): Observable<ISubject[]> {
    const headers = this.buildRequestHeaders()
    return this.httpClient.get<ISubject[]>('https://localhost:7067/api/subjects', { headers });
  }

  private buildRequestHeaders() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }
}
