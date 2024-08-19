import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EventsService } from '../../service/events.service';
import { catchError, EMPTY, Subject, takeUntil, tap } from 'rxjs';
import { IEvent } from '../../models/event';

@Component({
  selector: 'app-add-edit-event',
  templateUrl: './add-edit-event.component.html',
  styleUrls: ['./add-edit-event.component.scss']
})
export class AddEditEventComponent {
  @Input() title: string = ''
  @Output() closeAddEdit = new EventEmitter<boolean>();

  public formGroup: FormGroup;
  public errorMessage: string | undefined;
  
  private ngUnsubscribe = new Subject<void>();

  constructor(
    @Inject(FormBuilder) private readonly formBuilder: FormBuilder,
    @Inject(EventsService) private readonly eventsService: EventsService,)
  {
    this.formGroup = this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(100)]],
      location: ['', [Validators.required, Validators.maxLength(100)]],
      image: ['', [Validators.required, Validators.maxLength(100)]],
      startDate: [new Date(), [Validators.required]],
      endDate: [new Date(), [Validators.required]],
    });
  }

  public submit() {
    const payload = this.formGroup.getRawValue() as IEvent;
    this.eventsService.addEvent(payload)
      .pipe(
        tap(() => {
          this.closeAddEdit.emit(true);
        }),
        catchError((error) => {
          console.error(error);
          this.errorMessage = "Something went wrong, please try again.";
          return EMPTY;
        }),
        takeUntil(this.ngUnsubscribe)
      ).subscribe();
  }

  public ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
