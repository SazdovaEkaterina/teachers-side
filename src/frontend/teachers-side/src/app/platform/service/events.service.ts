import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IEvent } from '../models/event';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  constructor(
    @Inject(HttpClient) private readonly httpClient: HttpClient
  ) { }

  public getEvents(): Observable<IEvent[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.httpClient.get<IEvent[]>('https://localhost:7067/api/events', { headers });
  }

}
