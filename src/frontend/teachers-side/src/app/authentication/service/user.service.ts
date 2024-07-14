import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public isLoggedIn(): boolean {
    return !!localStorage.getItem('expiration')
      && new Date(localStorage.getItem('expiration') ?? '') > new Date()
      && !!localStorage.getItem('token');
  }
  
}
