import { IUser } from 'src/app/authentication/models/user';

export interface IEvent {
  id: number;
  creator: IUser;
  title: string;
  location: string;
  description: string;
  image: File | null;
  imagePath: string | null;
  startDate: Date;
  endDate: Date;
}
