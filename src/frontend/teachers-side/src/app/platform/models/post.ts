import { IUser } from "src/app/authentication/models/user";

export interface IPost {
    id: number,
    creator: IUser;
    title: string;
    content: string;
    dateCreated: Date;
    lastEdited: Date;
}