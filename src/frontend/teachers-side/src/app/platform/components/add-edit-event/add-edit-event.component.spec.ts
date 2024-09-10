import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditEventComponent } from './add-edit-event.component';

describe('AddEditEventComponent', () => {
  let component: AddEditEventComponent;
  let fixture: ComponentFixture<AddEditEventComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddEditEventComponent],
    });
    fixture = TestBed.createComponent(AddEditEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
