import { Injectable } from '@angular/core';

import { HttpClient }
from '@angular/common/http';

import { Observable }
from 'rxjs';

import { Attendance }
from '../models/attendance';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {

  private apiUrl =
  'http://localhost:5161/api/Attendance';

  constructor(
    private http: HttpClient
  ) {}

  getAttendance():
  Observable<Attendance[]> {

    return this.http.get<
      Attendance[]
    >(this.apiUrl);

  }

  getAttendanceById(id:number){

    return this.http.get<
      Attendance
    >(`${this.apiUrl}/${id}`);

  }

  createAttendance(
    attendance: Attendance
  ){

    return this.http.post(
      this.apiUrl,
      attendance
    );

  }

  updateAttendance(
    id:number,
    attendance:Attendance
  ){

    return this.http.put(
      `${this.apiUrl}/${id}`,
      attendance
    );

  }

  deleteAttendance(
    id:number
  ){

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );

  }

  getMyAttendance() {

  return this.http.get<any[]>(

    `${this.apiUrl}/my-attendance`

  );

}

}