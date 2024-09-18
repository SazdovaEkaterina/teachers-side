import { IUser } from 'src/app/authentication/models/user';

export interface IMaterial {
  id: number;
  subject: {
    id: number;
  };
  creator: IUser;
  dateCreated: Date;
  fileTitle: string;
  filePath: string;
  fileType: number;
}
