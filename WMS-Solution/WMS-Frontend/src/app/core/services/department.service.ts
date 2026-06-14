import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Department }
from '../models/department';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  private apiUrl =
    'http://localhost:5161/api/Department';

  constructor(
    private http: HttpClient
  ) {}

  getDepartments():
  Observable<Department[]> {

    return this.http.get<Department[]>(
      this.apiUrl
    );

  }

  getDepartmentById(id:number) {

    return this.http.get<Department>(
      `${this.apiUrl}/${id}`
    );

  }

  createDepartment(
    department:Department
  ) {

    return this.http.post(
      this.apiUrl,
      department
    );

  }

  updateDepartment(
    id:number,
    department:Department
  ) {

    return this.http.put(
      `${this.apiUrl}/${id}`,
      department
    );

  }

  deleteDepartment(id:number) {

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

}