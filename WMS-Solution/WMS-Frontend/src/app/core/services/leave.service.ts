import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Leave }
from '../models/leave';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {

  private apiUrl =
  'http://localhost:5161/api/Leave';

  constructor(
    private http: HttpClient
  ) {}

  getLeaves():
  Observable<Leave[]> {

    return this.http.get<
      Leave[]
    >(this.apiUrl);

  }

  getLeaveById(id:number){

    return this.http.get<Leave>(
      `${this.apiUrl}/${id}`
    );

  }

  createLeave(leave:Leave){

    return this.http.post(
      this.apiUrl,
      leave,
      {
        responseType: 'text'
      }
    );

  }

  updateLeave(
    id:number,
    leave:Leave
  ){

    return this.http.put(
      `${this.apiUrl}/${id}`,
      leave
    );

  }

  deleteLeave(id:number){

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

  approveLeave(
    leaveId:number,
    managerId:number
  ){

    return this.http.put(
      `${this.apiUrl}/approve/${leaveId}?managerId=${managerId}`,
      {}
    );

  }

  rejectLeave(
    leaveId:number,
    managerId:number
  ){

    return this.http.put(
      `${this.apiUrl}/reject/${leaveId}?managerId=${managerId}`,
      {}
    );

  }

  cancelLeave(id:number){

    return this.http.put(
      `${this.apiUrl}/cancel/${id}`,
      {}
    );

  }

  getMyLeaves() {

  return this.http.get<any[]>(

    `${this.apiUrl}/my-leaves`

  );

}

}