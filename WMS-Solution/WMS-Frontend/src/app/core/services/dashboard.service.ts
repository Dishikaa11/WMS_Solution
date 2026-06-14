import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { DashboardSummary }
from '../models/dashboard-summary';

import { EmployeeDashboard }
from '../models/employee-dashboard';

@Injectable({
  providedIn:'root'
})
export class DashboardService {

  private apiUrl =
  'https://wms-api-dishika-f8dtfnghfqezbxf5.centralindia-01.azurewebsites.net/api/Dashboard';

  constructor(
    private http: HttpClient
  ){}

  getSummary():
  Observable<DashboardSummary>{

    return this.http.get<DashboardSummary>(

      `${this.apiUrl}/summary`

    );

  }

  getEmployeeDashboard():

  Observable<EmployeeDashboard>{

    return this.http.get<EmployeeDashboard>(

      `${this.apiUrl}/employee`

    );

  }

}
