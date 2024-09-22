import { Inject, Injectable } from '@angular/core';
import { IMaterial } from '../models/material';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/authentication/service/user.service';

@Injectable({
  providedIn: 'root',
})
export class MaterialsService {
  constructor(
    @Inject(HttpClient) private readonly httpClient: HttpClient,
    @Inject(UserService) private readonly userService: UserService
  ) {}

  public getMaterials(
    subjectCategory: number,
    subjectName: string
  ): Observable<IMaterial[]> {
    const headers = this.buildRequestHeaders();
    return this.httpClient.get<IMaterial[]>(
      `https://localhost:7067/api/materials/${subjectCategory}/${subjectName}`,
      { headers }
    );
  }

  public addMaterial(formData: FormData): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    headers.set("type", "application/json");
    const creator = this.userService.getUser();
    if (creator) {
      formData.append("creatorDto", JSON.stringify(creator));
    }
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/materials/add`,
      formData, 
      { headers }
    );
  }

  public editMaterial(formData: FormData): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    headers.set("type", "application/json");
    const creator = this.userService.getUser();
    if (creator) {
      formData.append("creatorDto", JSON.stringify(creator));
    }
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/materials/${formData.get("id")}/edit`,
      formData,
      { headers }
    );
  }

  public deleteMaterial(materialId: number): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    return this.httpClient.delete<boolean>(
      `https://localhost:7067/api/materials/${materialId}`,
      { headers }
    );
  }

  private buildRequestHeaders() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }
}
