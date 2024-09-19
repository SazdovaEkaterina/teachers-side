import { Injectable } from '@angular/core';
import { IUser } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public isLoggedIn(): boolean {
    return !!localStorage.getItem('expiration')
      && new Date(localStorage.getItem('expiration') ?? '') > new Date()
      && !!localStorage.getItem('token');
  }

  public getUser(): IUser | null {
    const token = localStorage.getItem('token')
    if(token === null) return null;
    const decodedToken = this.getDecodedToken(token);
    const user: IUser = {
      firstName: decodedToken.first_name,
      lastName: decodedToken.last_name,
      email: decodedToken.email,
    }
    return user;
  }

  private getDecodedToken(token: string): any {
    let user: IUser;
    try {
      user = JSON.parse(atob(token.split('.')[1]));
    } catch(Error) {
      return null;
    }
    return user;
  }
  
}
