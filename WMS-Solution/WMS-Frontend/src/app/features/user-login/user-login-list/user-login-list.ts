import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { UserLoginService } from '../../../core/services/user-login.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { RoleService } from '../../../core/services/role.service';

import { UserLogin } from '../../../core/models/user-login';
import { Employee } from '../../../core/models/employee';
import { Role } from '../../../core/models/role';

@Component({
  selector: 'app-user-login-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-login-list.html',
  styleUrl: './user-login-list.scss'
})
export class UserLoginList implements OnInit {

  users: UserLogin[] = [];
  employees: Employee[] = [];
  roles: Role[] = [];

  constructor(
    private service: UserLoginService,
    private employeeService: EmployeeService,
    private roleService: RoleService,
    private router: Router
  ) {}

  ngOnInit(): void {

    this.loadUsers();
    this.loadEmployees();
    this.loadRoles();

  }

  loadUsers(): void {

    this.service.getUsers().subscribe({

      next: (data) => {

        this.users = data;

      },

      error: (err) => {

        console.log(err);

      }

    });

  }

  loadEmployees(): void {

    this.employeeService.getEmployees().subscribe({

      next: (data) => {

        this.employees = data;

      },

      error: (err) => {

        console.log(err);

      }

    });

  }

  loadRoles(): void {

    this.roleService.getRoles().subscribe({

      next: (data) => {

        this.roles = data;

      },

      error: (err) => {

        console.log(err);

      }

    });

  }

  addUser(): void {

    this.router.navigate([
      '/user-logins/add'
    ]);

  }

  editUser(id: number): void {

    this.router.navigate([
      '/user-logins/edit',
      id
    ]);

  }

  deleteUser(id: number): void {

    if (confirm('Delete User Login?')) {

      this.service
        .deleteUser(id)
        .subscribe({

          next: () => {

            this.loadUsers();

          },

          error: (err) => {

            console.log(err);

          }

        });

    }

  }

  getEmployeeName(employeeId: number): string {

    const employee = this.employees.find(
      x => x.employeeId === employeeId
    );

    return employee
      ? `${employee.firstName} ${employee.lastName}`
      : '-';

  }

  getRoleName(roleId: number): string {

    const role = this.roles.find(
      x => x.roleId === roleId
    );

    return role
      ? role.roleName
      : '-';

  }

}