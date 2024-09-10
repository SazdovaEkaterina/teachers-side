import { TestBed } from '@angular/core/testing';

import { ForumPostsService } from './forum-posts.service';

describe('ForumPostsService', () => {
  let service: ForumPostsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ForumPostsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
