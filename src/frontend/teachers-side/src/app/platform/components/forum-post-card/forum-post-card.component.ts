import { Component, Input } from '@angular/core';
import { IPost } from '../../models/post';

@Component({
  selector: 'app-forum-post-card',
  templateUrl: './forum-post-card.component.html',
  styleUrls: ['./forum-post-card.component.scss'],
})
export class ForumPostCardComponent {
  @Input() forumPost: IPost | undefined = {
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
}
