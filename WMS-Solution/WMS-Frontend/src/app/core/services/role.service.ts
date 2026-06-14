import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Role } from '../models/role';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private apiUrl =
  'http://localhost:5161/api/Role';

  constructor(
    private http: HttpClient
  ) {}

  getRoles():
  Observable<Role[]> {

    return this.http.get<Role[]>(
      this.apiUrl
    );

  }

  getRoleById(id:number){

    return this.http.get<Role>(
      `${this.apiUrl}/${id}`
    );

  }

  createRole(role:Role){

    return this.http.post(
      this.apiUrl,
      role
    );

  }

  updateRole(
    id:number,
    role:Role
  ){

    return this.http.put(
      `${this.apiUrl}/${id}`,
      role
    );

  }

  deleteRole(id:number){

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

}