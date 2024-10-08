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
import { UserService } from 'src/app/authentication/service/user.service';

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
  public selectedFile: File | null = null;
  public imagePreview: string | null = null;

  private ngUnsubscribe = new Subject<void>();

  constructor(
    @Inject(FormBuilder) private readonly formBuilder: FormBuilder,
    @Inject(EventsService) private readonly eventsService: EventsService,
    @Inject(UserService) private readonly userService: UserService
  ) {
    this.formGroup = this.formBuilder.group({});
  }

  public ngOnInit(): void {
    this.isEditMode = this.event ? true : false;
    this.initializeForm();
    this.title = this.event ? 'Edit Event' : 'Add New Event';
    this.isEditMode = this.event ? true : false;
  }

  public submit() {
    const formData = new FormData();

    const id = this.event?.id ?? 0;
    formData.append('id', id.toString());
    formData.append('title', this.formGroup.get('title')?.value);
    formData.append('description', this.formGroup.get('description')?.value);
    formData.append('location', this.formGroup.get('location')?.value);

    const startDateLocal = this.formGroup.get('startDate')?.value;
    const startDateUtc = new Date(`${startDateLocal}Z`);
    const startDateIsoString = new Date(startDateUtc.getTime() + startDateUtc.getTimezoneOffset() * 60000).toISOString();

    const endDateLocal = this.formGroup.get('endDate')?.value;
    const endDateUtc = new Date(`${endDateLocal}Z`);
    const endDateIsoString = new Date(endDateUtc.getTime() + endDateUtc.getTimezoneOffset() * 60000).toISOString();

    formData.append('startDate', startDateIsoString);
    formData.append('endDate', endDateIsoString);

    if (this.selectedFile) {
      formData.append('image', this.selectedFile, this.selectedFile.name);
    }
    
    this.submitEvent(formData);
  }

  public onImageUpload(event: any) {
    const file = event.target.files[0] as File;

    if (file) {
      const validImageTypes = ['image/jpeg', 'image/png', 'image/jpg', 'image/gif'];
  
      if (!validImageTypes.includes(file.type.toLocaleLowerCase())) {
        this.errorMessage = 'Invalid file type. Please upload an image of type JPG, PNG, JPEG or GIF.';
        this.selectedFile = null;
        this.imagePreview = null;
        return;
      } else {
        this.errorMessage = '';
      }
    }

    this.selectedFile = file;
    this.imagePreview = URL.createObjectURL(file);
  }

  public ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  private submitEvent(formData: FormData) {
    if (this.isEditMode) {
      this.eventsService
        .editEvent(formData)
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
        .addEvent(formData)
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
      image: [null, this.isEditMode ? null : [Validators.required]],
      startDate: [this.event?.startDate ?? new Date(), [Validators.required]],
      endDate: [this.event?.endDate ?? new Date(), [Validators.required]],
    });

    if (this.isEditMode && this.event?.imagePath) {
      this.imagePreview = 'https://localhost:7067' + this.event.imagePath;
    }
  }
}