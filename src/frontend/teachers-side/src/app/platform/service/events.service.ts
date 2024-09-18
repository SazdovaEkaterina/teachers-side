import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IEvent } from '../models/event';
import { UserService } from 'src/app/authentication/service/user.service';

@Injectable({
  providedIn: 'root',
})
export class EventsService {
  constructor(
    @Inject(HttpClient) private readonly httpClient: HttpClient,
    @Inject(UserService) private readonly userService: UserService
  ) {}

  public getEvents(): Observable<IEvent[]> {
    const headers = this.buildRequestHeaders();
    return this.httpClient.get<IEvent[]>('https://localhost:7067/api/events', {
      headers,
    });
  }

  public addEvent(event: IEvent): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    const creator = this.userService.getUser();
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/events/add`,
      { ...event, creator },
      { headers }
    );
  }

  public editEvent(event: IEvent): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    const creator = this.userService.getUser();
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/events/${event.id}/edit`,
      { ...event, creator },
      { headers }
    );
  }

  public deleteEvent(eventId: number): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    return this.httpClient.delete<boolean>(
      `https://localhost:7067/api/events/${eventId}`,
      { headers }
    );
  }

  private buildRequestHeaders() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }
}
