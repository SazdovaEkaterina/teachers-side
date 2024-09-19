import { Component, Input } from '@angular/core';
import { IEvent } from '../../models/event';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.scss']
})
export class EventCardComponent {
  @Input() event: IEvent = {
    id: 0,
    title: '',
    location: '',
    description: '',
    image: '',
    startDate: new Date(),
    endDate: new Date(),
  };
  formatDate(date: Date): string {
    const options: Intl.DateTimeFormatOptions = {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
      hour12: false
    };
    return new Intl.DateTimeFormat('en-GB', options).format(date);
  }
  public goToEventDetails () {
    
  }
}
