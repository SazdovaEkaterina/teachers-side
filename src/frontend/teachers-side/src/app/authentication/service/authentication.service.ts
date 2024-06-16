import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from '../models/login-model';
import { RegisterModel } from '../models/register-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(
    @Inject(HttpClient) private readonly httpClient: HttpClient
  ) { }

  public login(loginModel: LoginModel): Observable<(any)> {
    return this.httpClient.post<(any)>('https://localhost:7067/api/authentication/login', loginModel);
  }

  public register(registerModel: RegisterModel): Observable<(any)> {
    return this.httpClient.post<any>('https://localhost:7067/api/authentication/register', registerModel);
  }
}
