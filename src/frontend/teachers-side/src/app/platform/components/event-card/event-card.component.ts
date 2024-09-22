import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';

import { IEvent } from '../../models/event';
import { EventsService } from '../../service/events.service';
import { UserService } from 'src/app/authentication/service/user.service';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.scss'],
})
export class EventCardComponent {
  @Input() event: IEvent | undefined = {
    id: 0,
    creator: {
      firstName: '',
      lastName: '',
      email: '',
    },
    title: '',
    location: '',
    description: '',
    image: new File([], "empty.jpg", { type: "image/jpeg" }),
    imagePath: null,
    startDate: new Date(),
    endDate: new Date(),
  };
  @Output() editEvent = new EventEmitter<IEvent>();

  public isLoading: boolean = false;

  constructor(
    @Inject(EventsService) private readonly eventsService: EventsService,
    @Inject(UserService) private readonly userService: UserService
  ) {}

  public isCreator(event: IEvent) {
    return event.creator.email === this.userService.getUser()?.email;
  }

  public handleEdit(event: IEvent) {
    this.editEvent.emit(event);
  }

  public handleDelete(eventId: number) {
    this.isLoading = true;
    this.eventsService.deleteEvent(eventId).subscribe({
      error: (error: any) => {
        console.error('Error deleting event', error);
      },
      complete: () => {
        this.event = undefined;
        this.isLoading = false;
      },
    });
  }
}
