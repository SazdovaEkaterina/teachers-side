import { Component, Inject, Input } from '@angular/core';

import { IEvent } from '../../models/event';
import { EventsService } from '../../service/events.service';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.scss']
})
export class EventCardComponent {
  @Input() event: IEvent | undefined = {
    id: 0,
    title: '',
    location: '',
    description: '',
    image: '',
    startDate: new Date(),
    endDate: new Date(),
  };

  public isLoading: boolean = false;

  constructor (@Inject(EventsService) private readonly eventsService: EventsService) 
  { }

  public handleDelete(eventId: number) 
  {
    this.isLoading = true;
    this.eventsService.deleteEvent(eventId).subscribe({
      error: (error: any) => {
        console.error('Error deleting event', error);
      },
      complete: () => {
        this.event = undefined;
        this.isLoading = false;
      }
    });
  }
}
