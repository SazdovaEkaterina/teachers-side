import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { ISubject } from '../../models/subject';
import { IMaterial } from '../../models/material';
import { MaterialsService } from '../../service/materials.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject, tap, catchError, EMPTY, takeUntil } from 'rxjs';
import { UserService } from 'src/app/authentication/service/user.service';

@Component({
  selector: 'app-add-edit-material',
  templateUrl: './add-edit-material.component.html',
  styleUrls: ['./add-edit-material.component.scss'],
})
export class AddEditMaterialComponent {
  @Input() material: IMaterial | null = null;
  @Input() subject: ISubject = {
    id: 0,
    name: '',
    category: 0,
  };
  @Output() closeAddEdit = new EventEmitter<boolean>();

  public title: string = '';
  public isEditMode: boolean = false;
  public formGroup: FormGroup;
  public errorMessage: string | undefined;

  private ngUnsubscribe = new Subject<void>();

  constructor(
    @Inject(FormBuilder) private readonly formBuilder: FormBuilder,
    @Inject(MaterialsService)
    private readonly materialsService: MaterialsService,
    @Inject(UserService) private readonly userService: UserService
  ) {
    this.formGroup = this.formBuilder.group({});
  }

  public ngOnInit(): void {
    this.initializeForm();
    this.title = this.material ? 'Edit Material' : 'Add Material';
    this.isEditMode = this.material ? true : false;
  }

  public ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  public submit(): void {
    const id = this.material?.id ?? 0;
    const creator = this.userService.getUser;
    const payload = {
      ...this.formGroup.getRawValue(),
      creator,
      id,
      subject: this.subject,
    } as IMaterial;
    if (this.isEditMode) {
      this.materialsService
        .editMaterial(payload)
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
      this.materialsService
        .addMaterial(payload)
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

  private initializeForm(): void {
    this.formGroup = this.formBuilder.group({
      fileTitle: [this.material?.fileTitle ?? '', [Validators.required]],
      filePath: [this.material?.filePath ?? '', [Validators.required]],
      fileType: [this.material?.fileType ?? '', [Validators.required]],
    });
  }
}
