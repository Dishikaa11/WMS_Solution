import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { AttendanceService }
from '../../../core/services/attendance.service';

import { Attendance }
from '../../../core/models/attendance';

@Component({
  selector: 'app-attendance-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './attendance-list.html',
  styleUrl: './attendance-list.scss'
})
export class AttendanceList
implements OnInit {

  attendances: Attendance[] = [];
  role = localStorage.getItem('role');

  constructor(
    private attendanceService:
    AttendanceService,

    private router: Router
  ) {}

  ngOnInit(): void {

    this.loadAttendance();

  }

  loadAttendance() {

    this.attendanceService
      .getAttendance()
      .subscribe({

        next: (data) => {

          this.attendances = data;

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  addAttendance() {

    this.router.navigate([
      '/attendance/add'
    ]);

  }

  editAttendance(id:number) {

    this.router.navigate([
      '/attendance/edit',
      id
    ]);

  }

  deleteAttendance(id:number) {

    if(confirm(
      'Delete Attendance Record?'
    )){

      this.attendanceService
        .deleteAttendance(id)
        .subscribe({

          next: () => {

            this.loadAttendance();

          }

        });

    }

  }

}
