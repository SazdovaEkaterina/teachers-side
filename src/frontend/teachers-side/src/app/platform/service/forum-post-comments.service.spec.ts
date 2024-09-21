import { TestBed } from '@angular/core/testing';

import { ForumPostCommentsService } from './forum-post-comments.service';

describe('ForumPostCommentsService', () => {
  let service: ForumPostCommentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ForumPostCommentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
