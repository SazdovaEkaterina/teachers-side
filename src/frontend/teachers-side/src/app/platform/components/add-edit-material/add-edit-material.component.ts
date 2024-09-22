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
  public selectedFile: File | null = null;
  public materialPathPreview: string | null = null;

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
    this.isEditMode = this.material ? true : false;
    this.initializeForm();
    this.title = this.material ? 'Edit Material' : 'Add Material';
    this.materialPathPreview = this.material?.filePath || null;
  }

  public ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  public submit()
  {
    const formData = new FormData();

    const id = this.material?.id ?? 0;
    formData.append('id', id.toString());

    if(this.subject)
    {
      formData.append('subjectDto', JSON.stringify(this.subject));
    }
    
    formData.append('fileTitle', this.formGroup.get('fileTitle')?.value);
    formData.append('fileType', this.formGroup.get('fileType')?.value);

    if (this.selectedFile) {
      formData.append('file', this.selectedFile, this.selectedFile.name);
    }

    

    console.log(this.material?.file?.type)

    console.log(formData.get("file"))
    this.submitMaterial(formData);
  }

  public submitMaterial(formData: FormData): void {
    if (this.isEditMode) {
      this.materialsService
        .editMaterial(formData)
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
        .addMaterial(formData)
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
      fileType: [this.material?.fileType ?? 0, [Validators.required]],
      file: [null, this.isEditMode ? null : [Validators.required]],
    });

    if (this.isEditMode && this.material?.file) {
      this.materialPathPreview = 'https://localhost:7067' + this.material.filePath;
      this.selectedFile = this.material.file;
    }
  }

  public onFileUpload(event: any) {
    this.errorMessage = undefined;
    const file = event.target.files[0] as File;

    if (file) {
      const fileExtension = file.name.split('.').pop()?.toLowerCase();

      switch (fileExtension) {
        case 'pdf':
            this.formGroup.get('fileType')?.setValue(0); // PDF
            break;
        case 'docx':
            this.formGroup.get('fileType')?.setValue(1); // DOCX
            break;
        case 'xls':
            this.formGroup.get('fileType')?.setValue(2); // XLS
            break;
        case 'pptx':
            this.formGroup.get('fileType')?.setValue(3); // PPTX
            break;
        default:
            this.errorMessage = 'Invalid file type. Please upload a valid file.';
            this.selectedFile = null;
            this.materialPathPreview = null;
            return;
      }

      this.selectedFile = file;
      this.materialPathPreview = URL.createObjectURL(file);
    }
  }
}
