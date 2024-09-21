import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumPostCommentCardComponent } from './forum-post-comment-card.component';

describe('ForumPostCommentCardComponent', () => {
  let component: ForumPostCommentCardComponent;
  let fixture: ComponentFixture<ForumPostCommentCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ForumPostCommentCardComponent]
    });
    fixture = TestBed.createComponent(ForumPostCommentCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
