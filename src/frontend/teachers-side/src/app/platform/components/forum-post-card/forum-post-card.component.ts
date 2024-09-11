import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { IPost } from '../../models/post';
import { ForumPostsService } from '../../service/forum-posts.service';
import { UserService } from 'src/app/authentication/service/user.service';

@Component({
  selector: 'app-forum-post-card',
  templateUrl: './forum-post-card.component.html',
  styleUrls: ['./forum-post-card.component.scss'],
})
export class ForumPostCardComponent {
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
  @Output() editForumPost = new EventEmitter<IPost>();

  public isLoading: boolean = false;

  constructor(
    @Inject(ForumPostsService)
    private readonly forumPostsService: ForumPostsService,
    @Inject(UserService) private readonly userService: UserService
  ) {}

  public isCreator(forumPost: IPost) {
    return forumPost.creator.email === this.userService.getUser()?.email;
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
}
