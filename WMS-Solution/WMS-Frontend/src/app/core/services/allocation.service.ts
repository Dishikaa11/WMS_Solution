import { Injectable } from '@angular/core';

import {
  HttpClient
} from '@angular/common/http';

import {
  Observable
} from 'rxjs';

import {
  EmployeeProjectAllocation
} from '../models/EmployeeAllocation';

@Injectable({
  providedIn: 'root'
})
export class EmployeeProjectAllocationService {

  private apiUrl =
    'https://wms-api-dishika-f8dtfnghfqezbxf5.centralindia-01.azurewebsites.net/api/EmployeeProjectAllocation';

  constructor(
    private http: HttpClient
  ) {}

  getAllocations():
  Observable<EmployeeProjectAllocation[]> {

    return this.http.get<
      EmployeeProjectAllocation[]
    >(this.apiUrl);

  }

  getAllocationById(
    id: number
  ) {

    return this.http.get<
      EmployeeProjectAllocation
    >(
      `${this.apiUrl}/${id}`
    );

  }

  createAllocation(
    data: EmployeeProjectAllocation
  ) {

    return this.http.post(
      this.apiUrl,
      data
    );

  }

  updateAllocation(
    id: number,
    data: EmployeeProjectAllocation
  ) {

    return this.http.put(
      `${this.apiUrl}/${id}`,
      data
    );

  }

  deleteAllocation(
    id: number
  ) {

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

}
