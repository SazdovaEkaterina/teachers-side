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

  public goToEventDetails () {
    
  }
}
