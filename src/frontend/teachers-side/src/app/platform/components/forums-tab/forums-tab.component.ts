import { Component, Inject, OnInit } from '@angular/core';
import { ISubject } from '../../models/subject';
import { SubjectsService } from '../../service/subjects.service';
import { IPost } from '../../models/post';
import { ForumPostsService } from '../../service/forum-posts.service';
import { IComment } from '../../models/comment';

@Component({
  selector: 'app-forums-tab',
  templateUrl: './forums-tab.component.html',
  styleUrls: ['./forums-tab.component.scss'],
})
export class ForumsTabComponent implements OnInit {
  public subjects: ISubject[] = [];
  public isSubjectsLoading: boolean = true;
  public selectedSubjectId: number | undefined = undefined;

  public forumPosts: IPost[] = [];
  public isForumPostsLoading: boolean = true;

  public isAddEditForumPostFormOpen: boolean = false;
  public forumPostForEdit: IPost | null = null;

  public isAddEditCommentFormOpen: boolean = false;
  public commentForAddPostId: number = 0;
  public commentForEdit: IComment | null = null;
  public commentsChanged: boolean = false;

  constructor(
    @Inject(SubjectsService) private readonly subjectsService: SubjectsService,
    @Inject(ForumPostsService)
    private readonly forumPostsService: ForumPostsService
  ) {}

  ngOnInit(): void {
    this.loadSubjects();
  }

  public goToAddForumPost() {
    this.isAddEditForumPostFormOpen = true;
    this.forumPostForEdit = null;
  }

  public goToEditForumPost(forumPost: IPost) {
    this.isAddEditForumPostFormOpen = true;
    this.forumPostForEdit = forumPost;
  }

  public closeAddEditEvent(changed: boolean) {
    if (changed) this.loadForumPosts();
    this.isAddEditForumPostFormOpen = false;
  }

  public onSubjectSelect(event: any) {
    this.selectedSubjectId = event.target.value;
    this.loadForumPosts();
  }

  public goToAddForumPostComment(forumPostId: number){
    this.isAddEditCommentFormOpen = true;
    this.commentForAddPostId = forumPostId;
    this.commentForEdit = null;
  }

  public goToEditForumPostComment(forumPostComment: IComment){
    this.isAddEditCommentFormOpen = true;
    this.commentForAddPostId = forumPostComment.postId;
    this.commentForEdit = forumPostComment;
  }

  public closeAddEditComment(changed: boolean) {
    if (changed) this.commentsChanged = changed;
    this.isAddEditCommentFormOpen = false;
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

  private loadForumPosts() {
    this.forumPostsService.getPosts(this.selectedSubjectId ?? 0).subscribe({
      next: (data: IPost[]) => {
        this.forumPosts = data;
      },
      error: (error: any) => {
        console.error('Error fetching forum posts', error);
      },
      complete: () => {
        this.isForumPostsLoading = false;
      },
    });
  }
}
