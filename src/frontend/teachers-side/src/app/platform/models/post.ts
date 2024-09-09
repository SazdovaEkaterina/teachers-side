import { IUser } from "src/app/authentication/models/user";

export interface IPost {
    creator: IUser;
    title: string;
    content: string;
    dateCreated: Date;
    lastEdited: Date;
}