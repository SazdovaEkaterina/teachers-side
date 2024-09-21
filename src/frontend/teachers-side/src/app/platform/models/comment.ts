import { IUser } from 'src/app/authentication/models/user';

export interface IComment {
  id: number;
  postId: number;
  creator: IUser;
  title: string;
  content: string;
  dateCreated: Date;
  lastEdited: Date;
}
