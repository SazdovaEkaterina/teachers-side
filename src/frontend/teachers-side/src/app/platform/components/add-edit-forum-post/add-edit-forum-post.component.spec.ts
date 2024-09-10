import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditForumPostComponent } from './add-edit-forum-post.component';

describe('AddEditForumPostComponent', () => {
  let component: AddEditForumPostComponent;
  let fixture: ComponentFixture<AddEditForumPostComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddEditForumPostComponent]
    });
    fixture = TestBed.createComponent(AddEditForumPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
