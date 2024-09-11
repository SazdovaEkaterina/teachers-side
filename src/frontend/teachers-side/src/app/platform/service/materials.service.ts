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

  public addMaterial(material: IMaterial): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    const creator = this.userService.getUser();
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/materials/add`,
      { ...material, creator },
      { headers }
    );
  }

  public editMaterial(material: IMaterial): Observable<boolean> {
    const headers = this.buildRequestHeaders();
    const creator = this.userService.getUser();
    return this.httpClient.post<boolean>(
      `https://localhost:7067/api/materials/${material.id}/edit`,
      { ...material, creator },
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
