import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { DepartmentService }
from '../../../core/services/department.service';

import { Department }
from '../../../core/models/department';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './department-list.html',
  styleUrl: './department-list.scss'
})
export class DepartmentList implements OnInit {

  departments: Department[] = [];

  constructor(
    private departmentService: DepartmentService,
    private router: Router
  ) {}

  ngOnInit(): void {

    this.loadDepartments();

  }

  loadDepartments() {

    this.departmentService
      .getDepartments()
      .subscribe({

        next: (data) => {

          this.departments = data;

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  addDepartment() {

    this.router.navigate(
      ['/departments/add']
    );

  }

  editDepartment(id: number) {

    this.router.navigate(
      ['/departments/edit', id]
    );

  }

  deleteDepartment(id: number) {

    if (confirm('Delete Department?')) {

      this.departmentService
        .deleteDepartment(id)
        .subscribe({

          next: () => {

            this.loadDepartments();

          },

          error: (err) => {

            console.log(err);

          }

        });

    }

  }

}