import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditForumPostCommentComponent } from './add-edit-forum-post-comment.component';

describe('AddEditForumPostCommentComponent', () => {
  let component: AddEditForumPostCommentComponent;
  let fixture: ComponentFixture<AddEditForumPostCommentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddEditForumPostCommentComponent]
    });
    fixture = TestBed.createComponent(AddEditForumPostCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
