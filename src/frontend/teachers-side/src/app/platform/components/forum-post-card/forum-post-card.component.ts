import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
} from '@angular/core';
import { IPost } from '../../models/post';
import { ForumPostsService } from '../../service/forum-posts.service';
import { UserService } from 'src/app/authentication/service/user.service';
import { IComment } from '../../models/comment';
import { ForumPostCommentsService } from '../../service/forum-post-comments.service';

@Component({
  selector: 'app-forum-post-card',
  templateUrl: './forum-post-card.component.html',
  styleUrls: ['./forum-post-card.component.scss'],
})
export class ForumPostCardComponent implements OnChanges {
  @Input() forumPost: IPost | undefined = {
    id: 0,
    creator: {
      firstName: '',
      lastName: '',
      email: '',
    },
    title: '',
    content: '',
    dateCreated: new Date(),
    lastEdited: new Date(),
  };
  @Input() forumId: number = 0;
  @Input() commentsChanged: boolean = false;
  @Output() editForumPost = new EventEmitter<IPost>();
  @Output() addComment = new EventEmitter<number>();
  @Output() editComment = new EventEmitter<IComment>();

  public isLoading: boolean = false;

  public comments: IComment[] = [];
  public isCommentsLoading: boolean = true;

  constructor(
    @Inject(ForumPostsService)
    private readonly forumPostsService: ForumPostsService,
    @Inject(ForumPostCommentsService)
    private readonly forumPostCommentsService: ForumPostCommentsService,
    @Inject(UserService) private readonly userService: UserService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (this.commentsChanged) this.loadComments();
  }

  public isCreator(forumPost: IPost) {
    return forumPost.creator.email === this.userService.getUser()?.email;
  }

  public hasBeenEdited() {
    return this.forumPost?.dateCreated !== this.forumPost?.lastEdited;
  }

  public handleEdit(forumPost: IPost) {
    this.editForumPost.emit(forumPost);
  }

  public handleDelete(forumPostId: number) {
    this.isLoading = true;
    this.forumPostsService.deleteForumPost(forumPostId).subscribe({
      error: (error: any) => {
        console.error('Error deleting forum post', error);
      },
      complete: () => {
        this.forumPost = undefined;
        this.isLoading = false;
      },
    });
  }

  public handleCommentsPanelOpen() {
    this.loadComments();
  }

  public handleAddComment() {
    this.addComment.emit(this.forumPost?.id ?? 0);
  }

  public handleEditComment(comment: IComment) {
    this.editComment.emit(comment);
  }

  private loadComments() {
    this.forumPostCommentsService
      .getPostComments(this.forumPost?.id ?? 0)
      .subscribe({
        next: (data: IComment[]) => {
          this.comments = data;
        },
        error: (error: any) => {
          console.error('Error fetching comments', error);
        },
        complete: () => {
          this.isCommentsLoading = false;
        },
      });
  }
}
