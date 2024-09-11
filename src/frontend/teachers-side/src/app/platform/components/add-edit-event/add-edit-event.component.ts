import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EventsService } from '../../service/events.service';
import { catchError, EMPTY, Subject, takeUntil, tap } from 'rxjs';
import { IEvent } from '../../models/event';

@Component({
  selector: 'app-add-edit-event',
  templateUrl: './add-edit-event.component.html',
  styleUrls: ['./add-edit-event.component.scss'],
})
export class AddEditEventComponent implements OnInit, OnDestroy {
  @Input() event: IEvent | null = null;
  @Output() closeAddEdit = new EventEmitter<boolean>();

  public title: string = '';
  public isEditMode: boolean = false;
  public formGroup: FormGroup;
  public errorMessage: string | undefined;

  private ngUnsubscribe = new Subject<void>();

  constructor(
    @Inject(FormBuilder) private readonly formBuilder: FormBuilder,
    @Inject(EventsService) private readonly eventsService: EventsService
  ) {
    this.formGroup = this.formBuilder.group({});
  }

  public ngOnInit(): void {
    this.initializeForm();
    this.title = this.event ? 'Edit Event' : 'Add Event';
    this.isEditMode = this.event ? true : false;
  }

  public submit() {
    const id = this.event?.id ?? 0;
    const payload = { ...this.formGroup.getRawValue(), id } as IEvent;
    if (this.isEditMode) {
      this.eventsService
        .editEvent(payload)
        .pipe(
          tap((result) => {
            this.closeAddEdit.emit(result);
            if (!result)
              this.errorMessage =
                'The edit was unsuccessful, please try again.';
          }),
          catchError((error) => {
            console.error(error);
            this.errorMessage = 'Something went wrong, please try again.';
            return EMPTY;
          }),
          takeUntil(this.ngUnsubscribe)
        )
        .subscribe();
    } else {
      this.eventsService
        .addEvent(payload)
        .pipe(
          tap((result) => {
            this.closeAddEdit.emit(result);
            if (!result)
              this.errorMessage = 'The add was unsuccessful, please try again.';
          }),
          catchError((error) => {
            console.error(error);
            this.errorMessage = 'Something went wrong, please try again.';
            return EMPTY;
          }),
          takeUntil(this.ngUnsubscribe)
        )
        .subscribe();
    }
  }

  public ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  private initializeForm() {
    this.formGroup = this.formBuilder.group({
      title: [
        this.event?.title ?? '',
        [Validators.required, Validators.maxLength(100)],
      ],
      description: [
        this.event?.description ?? '',
        [Validators.required, Validators.maxLength(100)],
      ],
      location: [
        this.event?.location ?? '',
        [Validators.required, Validators.maxLength(100)],
      ],
      image: [
        this.event?.image ?? '',
        [Validators.required, Validators.maxLength(100)],
      ],
      startDate: [this.event?.startDate ?? new Date(), [Validators.required]],
      endDate: [this.event?.endDate ?? new Date(), [Validators.required]],
    });
  }
}
