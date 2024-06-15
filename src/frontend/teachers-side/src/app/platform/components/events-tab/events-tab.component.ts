import { Component, Inject } from '@angular/core';
import { IEvent } from '../../models/event';
import { EventsService } from '../../service/events.service';

@Component({
  selector: 'app-events-tab',
  templateUrl: './events-tab.component.html',
  styleUrls: ['./events-tab.component.scss']
})
export class EventsTabComponent {
  public events: IEvent[] = [];

  constructor (
    @Inject(EventsService) private readonly eventsService: EventsService,) {
  }

  public openAddEventDialog () {

  }

  public goToEventDetails () {
    
  }
}
