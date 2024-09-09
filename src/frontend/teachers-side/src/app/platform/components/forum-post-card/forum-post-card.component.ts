import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IPost } from '../../models/post';

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

  public handleEdit(forumPost: IPost) {
    this.editForumPost.emit(forumPost);
  }
}
