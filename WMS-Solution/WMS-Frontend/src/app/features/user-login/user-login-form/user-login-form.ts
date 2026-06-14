import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';

import { CommonModule } from '@angular/common';

import {
  ActivatedRoute,
  Router
} from '@angular/router';

import { EmployeeService } from '../../../core/services/employee.service';
import { RoleService } from '../../../core/services/role.service';
import { UserLoginService } from '../../../core/services/user-login.service';

@Component({
  selector: 'app-user-login-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './user-login-form.html',
  styleUrl: './user-login-form.scss'
})
export class UserLoginForm implements OnInit {

  userForm!: FormGroup;

  employees: any[] = [];
  roles: any[] = [];

  userId = 0;
  isEditMode = false;

  constructor(

    private fb: FormBuilder,

    private employeeService: EmployeeService,

    private roleService: RoleService,

    private userService: UserLoginService,

    private router: Router,

    private route: ActivatedRoute

  ) {}

  ngOnInit(): void {

    this.userForm = this.fb.group({

      username: [
        '',
        Validators.required
      ],

      passwordHash: [
        '',
        Validators.required
      ],

      employeeId: [
        '',
        Validators.required
      ],

      roleId: [
        '',
        Validators.required
      ]

    });

    this.loadEmployees();
    this.loadRoles();

    const id =
      this.route.snapshot.paramMap.get('id');

    if (id) {

      this.isEditMode = true;

      this.userId = +id;

      this.loadUser();

    }

  }

  loadEmployees(): void {

    this.employeeService
      .getEmployees()
      .subscribe({

        next: (data) => {

          this.employees = data;

        }

      });

  }

  loadRoles(): void {

    this.roleService
      .getRoles()
      .subscribe({

        next: (data) => {

          this.roles = data;

        }

      });

  }

  loadUser(): void {

    this.userService
      .getUserById(this.userId)
      .subscribe({

        next: (data) => {

          this.userForm.patchValue(data);

        }

      });

  }

  onSubmit(): void {

    if (this.userForm.invalid)
      return;

    if (this.isEditMode) {

      this.userService
        .updateUser(
          this.userId,
          this.userForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'User Updated Successfully'
            );

            this.router.navigate([
              '/user-logins'
            ]);

          }

        });

    }

    else {

      this.userService
        .createUser(
          this.userForm.value
        )
        .subscribe({

          next: () => {

            alert(
              'User Created Successfully'
            );

            this.router.navigate([
              '/user-logins'
            ]);

          }

        });

    }

  }

  goBack(): void {

    this.router.navigate([
      '/user-logins'
    ]);

  }

}