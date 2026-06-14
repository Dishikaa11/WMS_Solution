import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';

import { CommonModule } from '@angular/common';

import { EmployeeService }
from '../../../core/services/employee.service';

import { Employee }
from '../../../core/models/employee';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-list.html',
  styleUrl: './employee-list.scss'
})
export class EmployeeList
implements OnInit {

  employees: Employee[] = [];
  allEmployees: Employee[] = [];

  searchText = '';

  constructor(

    private router: Router,

    private employeeService:
    EmployeeService

  ) {}

  ngOnInit(): void {

    this.loadEmployees();


  }

  loadEmployees() {

    this.employeeService
      .getEmployees()
      .subscribe({

        next: (data) => {

          this.employees = data;
          this.allEmployees = data;

        },

        error: (error) => {

          console.log(error);

        }

      });

  }

  addEmployee() {

    this.router.navigate(
      ['/employees/add']
    );

  }

  deleteEmployee(id:number){

  if(
    confirm(
      'Delete Employee?'
    )
  ){

    this.employeeService
      .deleteEmployee(id)
      .subscribe({

        next: () => {

          this.loadEmployees();

        }

      });

  }

}

editEmployee(id:number){

  this.router.navigate(
    ['/employees/edit', id]
  );

}
searchEmployees(){

  if(!this.searchText){

    this.employees =
    this.allEmployees;

    return;

  }

  this.employees =
  this.allEmployees.filter(

    employee =>

      employee.firstName
      .toLowerCase()
      .includes(
        this.searchText
        .toLowerCase()
      )

      ||

      employee.lastName
      .toLowerCase()
      .includes(
        this.searchText
        .toLowerCase()
      )

      ||

      employee.email
      .toLowerCase()
      .includes(
        this.searchText
        .toLowerCase()
      )

  );

}

}