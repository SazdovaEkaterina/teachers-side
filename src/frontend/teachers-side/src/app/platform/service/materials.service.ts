import { Inject, Injectable } from '@angular/core';
import { IMaterial } from '../models/material';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/authentication/service/user.service';

@Injectable({
  providedIn: 'root',
})
export class MaterialsService {
  constructor(@Inject(HttpClient) private readonly httpClient: HttpClient) {}

  public getMaterials(subjectCategory: number, subjectName: string): Observable<IMaterial[]> {
    const headers = this.buildRequestHeaders();
    return this.httpClient.get<IMaterial[]>(
      `https://localhost:7067/api/materials/${subjectCategory}/${subjectName}`,
      { headers }
    );
  }

  private buildRequestHeaders() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }
}
