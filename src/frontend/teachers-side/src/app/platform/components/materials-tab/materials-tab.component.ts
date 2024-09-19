import { Component, Inject } from '@angular/core';
import { IMaterial } from '../../models/material';
import { ISubject } from '../../models/subject';
import { SubjectsService } from '../../service/subjects.service';
import { MaterialsService } from '../../service/materials.service';
import {MatTabsModule} from '@angular/material/tabs';

@Component({
  selector: 'app-materials-tab',
  templateUrl: './materials-tab.component.html',
  styleUrls: ['./materials-tab.component.scss']
})
export class MaterialsTabComponent {
  public subjects: ISubject[] = [];
  public isSubjectsLoading: boolean = true;
  public selectedSubjectId: number | undefined = undefined;

  public materials: IMaterial[] = [];
  public isMaterialsLoading: boolean = true;

  public isAddEditMaterialFormOpen: boolean = false;
  public materialForEdit: IMaterial | null = null;

  constructor(
    @Inject(SubjectsService) private readonly subjectsService: SubjectsService,
    @Inject(MaterialsService)
    private readonly materialsService: MaterialsService
  ) {}

  ngOnInit(): void {
    this.loadSubjects();
  }

  public goToAddMaterial() {
    this.isAddEditMaterialFormOpen = true;
    this.materialForEdit = null;
  }

  public goToEditMaterial(material: IMaterial) {
    this.isAddEditMaterialFormOpen = true;
    this.materialForEdit = material;
  }

  public closeAddEditEvent(changed: boolean) {
    if (changed) this.loadMaterials();
    this.isAddEditMaterialFormOpen = false;
  }

  public onSubjectSelect(event: any) {
    this.selectedSubjectId = event.value;
    this.loadMaterials();
  }

  public getSelectedSubject(): ISubject {
    return (
      this.subjects.find(
        (subject) => subject.id === this.selectedSubjectId
      ) ?? { id: 0, name: '', category: 0 }
    );
  }

  private loadSubjects() {
    this.subjectsService.getSubjects().subscribe({
      next: (data: ISubject[]) => {
        this.subjects = data;
      },
      error: (error: any) => {
        console.error('Error fetching subjects', error);
      },
      complete: () => {
        this.isSubjectsLoading = false;
      },
    });
  }

  private loadMaterials() {
    const selectedSubject = this.getSelectedSubject();
    const { selectedSubjectCategory, selectedSubjectName } = {
      selectedSubjectCategory: selectedSubject?.category ?? 0,
      selectedSubjectName: selectedSubject?.name ?? '',
    };
    this.materialsService
      .getMaterials(selectedSubjectCategory, selectedSubjectName)
      .subscribe({
        next: (data: IMaterial[]) => {
          this.materials = data;
        },
        error: (error: any) => {
          console.error('Error fetching materials for subject', error);
        },
        complete: () => {
          this.isMaterialsLoading = false;
        },
      });
  }
}
