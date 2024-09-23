import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { IComment } from '../../models/comment';
import { UserService } from 'src/app/authentication/service/user.service';
import { ForumPostCommentsService } from '../../service/forum-post-comments.service';

@Component({
  selector: 'app-forum-post-comment-card',
  templateUrl: './forum-post-comment-card.component.html',
  styleUrls: ['./forum-post-comment-card.component.scss'],
})
export class ForumPostCommentCardComponent {
  @Input() comment: IComment | undefined = {
    id: 0,
    postId: 0,
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
  @Output() editComment = new EventEmitter<IComment>();

  public isLoading: boolean = false;

  constructor(
    @Inject(ForumPostCommentsService)
    private readonly forumPostCommentsService: ForumPostCommentsService,
    @Inject(UserService) private readonly userService: UserService
  ) {}

  public isCreator(comment: IComment) {
    return comment.creator.email === this.userService.getUser()?.email;
  }

  public hasBeenEdited() {
    return this.comment?.dateCreated !== this.comment?.lastEdited;
  }

  public handleEdit(comment: IComment) {
    this.editComment.emit(comment);
  }

  public handleDelete(commentId: number) {
    this.isLoading = true;
    this.forumPostCommentsService.deletePostComment(commentId).subscribe({
      error: (error: any) => {
        console.error('Error deleting post comment', error);
      },
      complete: () => {
        this.comment = undefined;
        this.isLoading = false;
      },
    });
  }
}
