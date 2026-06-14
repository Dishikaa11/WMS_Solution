import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
}
from '@angular/forms';

import { CommonModule } from '@angular/common';

import {
  Router,
  ActivatedRoute
}
from '@angular/router';

import { DepartmentService }
from '../../../core/services/department.service';

@Component({
  selector: 'app-department-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './department-form.html',
  styleUrl: './department-form.scss'
})
export class DepartmentForm
implements OnInit {

  departmentForm: FormGroup;

  departmentId = 0;

  isEditMode = false;

  constructor(

    private fb: FormBuilder,

    private departmentService:
    DepartmentService,

    private router: Router,

    private route: ActivatedRoute

  ) {

    this.departmentForm =
      this.fb.group({

        departmentName: [
          '',
          Validators.required
        ],

        description: [
          '',
          Validators.required
        ]

      });

  }

  ngOnInit(): void {

    const id =
      this.route.snapshot.paramMap.get('id');

    if (id) {

      this.isEditMode = true;

      this.departmentId = +id;

      this.departmentService
        .getDepartmentById(
          this.departmentId
        )
        .subscribe({

          next: (data) => {

            this.departmentForm.patchValue({

              departmentName:
                data.departmentName,

              description:
                data.description

            });

          },

          error: (err) => {

            console.log(err);

          }

        });

    }

  }

  goBack() {

    this.router.navigate(
      ['/departments']
    );

  }

  onSubmit() {

    if (
      !this.departmentForm.valid
    ) {
      return;
    }

    if (this.isEditMode) {

      this.departmentService
        .updateDepartment(

          this.departmentId,

          this.departmentForm.value

        )
        .subscribe({

          next: () => {

            alert(
              'Department Updated Successfully'
            );

            this.router.navigate([
              '/departments'
            ]);

          },

          error: (err) => {

            console.log(err);

            alert(
              'Error Updating Department'
            );

          }

        });

    }

    else {

      this.departmentService
        .createDepartment(
          this.departmentForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'Department Added Successfully'
            );

            this.router.navigate([
              '/departments'
            ]);

          },

          error: (err) => {

            console.log(err);

            alert(
              'Error Saving Department'
            );

          }

        });

    }

  }

}