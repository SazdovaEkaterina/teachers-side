import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { IMaterial } from '../../models/material';
import { ISubject } from '../../models/subject';
import { MaterialsService } from '../../service/materials.service';
import { UserService } from 'src/app/authentication/service/user.service';

@Component({
  selector: 'app-material-card',
  templateUrl: './material-card.component.html',
  styleUrls: ['./material-card.component.scss'],
})
export class MaterialCardComponent {
  @Input() material: IMaterial | undefined = {
    id: 0,
    subject: {
      id: 0,
    },
    creator: {
      firstName: '',
      lastName: '',
      email: '',
    },
    dateCreated: new Date(),
    fileTitle: '',
    filePath: '',
    file: null,
    fileType: 0,
  };
  @Input() subject: ISubject = {
    id: 0,
    name: '',
    category: 0,
  };
  @Output() editMaterial = new EventEmitter<IMaterial>();
  @Output() deleteMaterial = new EventEmitter<boolean>();

  public isLoading: boolean = false;

  constructor(
    @Inject(MaterialsService)
    private readonly materialsService: MaterialsService,
    @Inject(UserService) private readonly userService: UserService
  ) {}

  public isCreator(material: IMaterial) {
    return material.creator.email === this.userService.getUser()?.email;
  }

  public handleEdit(material: IMaterial) {
    this.editMaterial.emit(material);
  }

  public handleDelete(materialId: number) {
    this.isLoading = true;
    this.materialsService.deleteMaterial(materialId).subscribe({
      next: (result: boolean) => {
        this.deleteMaterial.emit(result);
      },
      error: (error: any) => {
        console.error('Error deleting material', error);
      },
      complete: () => {
        this.material = undefined;
        this.isLoading = false;
      },
    });
  }
}
