import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';

import { CommonModule } from '@angular/common';

import { Router, ActivatedRoute } from '@angular/router';

import { LeaveService } from '../../../core/services/leave.service';

@Component({
  selector: 'app-leave-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './leave-form.html',
  styleUrl: './leave-form.scss'
})
export class LeaveForm
implements OnInit {

  leaveForm: FormGroup;

  leaveId = 0;

  isEditMode = false;

  constructor(

    private fb: FormBuilder,

    private leaveService:
    LeaveService,

    private router: Router,

    private route: ActivatedRoute

  ) {

    this.leaveForm =
      this.fb.group({

        empId: [
          '',
          Validators.required
        ],

        leaveType: [
          '',
          Validators.required
        ],

        reason: [
          '',
          Validators.required
        ],

        fromDate: [
          '',
          Validators.required
        ],

        toDate: [
          '',
          Validators.required
        ],

        status: [
          'Pending'
        ],

        appliedOn: [
          new Date()
        ]

      });

  }

  ngOnInit(): void {

    const id =
      this.route.snapshot.paramMap.get('id');

    if(id){

      this.isEditMode = true;

      this.leaveId = +id;

      this.leaveService
        .getLeaveById(
          this.leaveId
        )
        .subscribe({

          next: (data) => {

            this.leaveForm
              .patchValue(data);

          }

        });

    }

  }

  goBack() {

    this.router.navigate([
      '/leaves'
    ]);

  }

  onSubmit() {

    if(!this.leaveForm.valid)
      return;

    if(this.isEditMode){

      this.leaveService
        .updateLeave(

          this.leaveId,

          this.leaveForm.value

        )
        .subscribe({

          next: () => {

            alert(
              'Leave Updated Successfully'
            );

            this.router.navigate([
              '/leaves'
            ]);

          }

        });

    }

    else{

      this.leaveService
        .createLeave(
          this.leaveForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'Leave Applied Successfully'
            );

            this.router.navigate([
              '/leaves'
            ]);

          }

        });

    }

  }

}
