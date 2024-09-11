import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditMaterialComponent } from './add-edit-material.component';

describe('AddEditMaterialComponent', () => {
  let component: AddEditMaterialComponent;
  let fixture: ComponentFixture<AddEditMaterialComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddEditMaterialComponent]
    });
    fixture = TestBed.createComponent(AddEditMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
