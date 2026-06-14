import { Injectable } from '@angular/core';

import { HttpClient }
from '@angular/common/http';

import { Observable }
from 'rxjs';

import { AuditLog }
from '../models/audit-log';

@Injectable({
  providedIn:'root'
})
export class AuditLogService {

  private apiUrl =
  'http://localhost:5161/api/AuditLog';

  constructor(
    private http: HttpClient
  ){}

  getLogs():
  Observable<AuditLog[]>{

    return this.http.get<
      AuditLog[]
    >(this.apiUrl);

  }

}