import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Project }
from '../models/project';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private apiUrl =
  'https://wms-api-dishika-f8dtfnghfqezbxf5.centralindia-01.azurewebsites.net/api/Project';

  constructor(
    private http: HttpClient
  ) {}

  getProjects():
  Observable<Project[]> {

    return this.http.get<Project[]>(
      this.apiUrl
    );

  }

  getProjectById(id:number){

    return this.http.get<Project>(
      `${this.apiUrl}/${id}`
    );

  }

  createProject(project:Project){

    return this.http.post(
      this.apiUrl,
      project
    );

  }

  updateProject(
    id:number,
    project:Project
  ){

    return this.http.put(
      `${this.apiUrl}/${id}`,
      project
    );

  }

  deleteProject(id:number){

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

}
