import { Component, Inject, OnInit } from '@angular/core';
import { IEvent } from '../../models/event';
import { EventsService } from '../../service/events.service';

@Component({
  selector: 'app-events-tab',
  templateUrl: './events-tab.component.html',
  styleUrls: ['./events-tab.component.scss'],
})
export class EventsTabComponent implements OnInit {
  public events: IEvent[] = [];
  public isLoading: boolean = true;
  public isAddEditEventFormOpen: boolean = false;
  public eventForEdit: IEvent | null = null;

  public goToAddEvent() {
    this.isAddEditEventFormOpen = true;
    this.eventForEdit = null;
  }

  public goToEditEvent(event: IEvent) {
    this.isAddEditEventFormOpen = true;
    this.eventForEdit = event;
  }

  public closeAddEditEvent(changed: boolean) {
    if (changed) this.loadEvents();
    this.isAddEditEventFormOpen = false;
  }

  constructor(
    @Inject(EventsService) private readonly eventsService: EventsService
  ) {}

  ngOnInit() {
    this.loadEvents();
  }

  loadEvents() {
    this.eventsService.getEvents().subscribe({
      next: (data: IEvent[]) => {
        this.events = data;
      },
      error: (error: any) => {
        console.error('Error fetching events', error);
      },
      complete: () => {
        this.isLoading = false;
      },
    });
  }
}
