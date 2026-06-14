import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Employee } from '../models/employee';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private apiUrl =
    `${environment.apiUrl}/Employee`;

  constructor(
    private http: HttpClient
  ){}

  getEmployees():Observable<Employee[]>{

    return this.http.get<Employee[]>(
      this.apiUrl
    );

  }

  getEmployeeById(id:number){

    return this.http.get<Employee>(
      `${this.apiUrl}/${id}`
    );

  }

  createEmployee(employee:Employee){

    return this.http.post(
      this.apiUrl,
      employee
    );

  }

  updateEmployee(
    id:number,
    employee:Employee
  ){

    return this.http.put(
      `${this.apiUrl}/${id}`,
      employee
    );

  }

  deleteEmployee(id:number){

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

}
