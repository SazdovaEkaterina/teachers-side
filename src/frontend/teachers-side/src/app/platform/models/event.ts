import { IUser } from 'src/app/authentication/models/user';

export interface IEvent {
  id: number;
  creator: IUser;
  title: string;
  location: string;
  description: string;
  image: string;
  startDate: Date;
  endDate: Date;
}
