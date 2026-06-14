import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { CommonModule } from '@angular/common';

import {
  Router,
  ActivatedRoute
} from '@angular/router';

import { AttendanceService }
from '../../../core/services/attendance.service';

@Component({
  selector: 'app-attendance-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './attendance-form.html',
  styleUrl: './attendance-form.scss'
})
export class AttendanceForm
implements OnInit {

  attendanceForm: FormGroup;

  attendanceId = 0;

  isEditMode = false;

  constructor(

    private fb: FormBuilder,

    private attendanceService:
    AttendanceService,

    private router: Router,

    private route: ActivatedRoute

  ) {

    this.attendanceForm =
      this.fb.group({

        empId: [
          '',
          Validators.required
        ],

        attendanceDate: [
          '',
          Validators.required
        ],

        checkIn: [
          '',
          Validators.required
        ],

        checkOut: [''],

        totalHours: [0],

        workMode: [
          'Office',
          Validators.required
        ]

      });

  }

  ngOnInit(): void {

    const id =
      this.route.snapshot.paramMap.get('id');

    if (id) {

      this.isEditMode = true;

      this.attendanceId = +id;

      this.attendanceService
        .getAttendanceById(
          this.attendanceId
        )
        .subscribe({

          next: (data) => {

            this.attendanceForm
              .patchValue(data);

          },

          error: (err) => {

            console.log(err);

          }

        });

    }

  }

  goBack() {

    this.router.navigate([
      '/attendance'
    ]);

  }

  onSubmit() {

    if (!this.attendanceForm.valid)
      return;

    if (this.isEditMode) {

      this.attendanceService
        .updateAttendance(
          this.attendanceId,
          this.attendanceForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'Attendance Updated Successfully'
            );

            this.router.navigate([
              '/attendance'
            ]);

          }

        });

    }

    else {

      this.attendanceService
        .createAttendance(
          this.attendanceForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'Attendance Added Successfully'
            );

            this.router.navigate([
              '/attendance'
            ]);

          }

        });

    }

  }

}
