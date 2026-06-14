import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
}
from '@angular/forms';

import { CommonModule } from '@angular/common';

import {
  Router,
  ActivatedRoute
}
from '@angular/router';

import {
  EmployeeProjectAllocationService
}
from '../../../core/services/allocation.service';

import {
  EmployeeService
}
from '../../../core/services/employee.service';

import {
  ProjectService
}
from '../../../core/services/project.service';

import {
  Employee
}
from '../../../core/models/employee';

import {
  Project
}
from '../../../core/models/project';

@Component({
  selector:'app-employee-allocation-form',
  standalone:true,
  imports:[
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl:'./allocation-form.html',
  styleUrl:'./allocation-form.scss'
})
export class EmployeeAllocationForm
implements OnInit {

  allocationForm!: FormGroup;

  employees: Employee[] = [];

  projects: Project[] = [];

  allocationId = 0;

  isEditMode = false;

  constructor(

    private fb: FormBuilder,

    private allocationService:
    EmployeeProjectAllocationService,

    private employeeService:
    EmployeeService,

    private projectService:
    ProjectService,

    private router: Router,

    private route: ActivatedRoute

  ){}

  ngOnInit(): void {

    this.allocationForm =
    this.fb.group({

      empId:[
        '',
        Validators.required
      ],

      projectId:[
        '',
        Validators.required
      ],

      assignedOn:[
        '',
        Validators.required
      ],

      status:[
        'Active',
        Validators.required
      ],

      createdBy:['Admin'],

      createDate:[
        new Date()
      ]

    });

    this.loadEmployees();

    this.loadProjects();

    const id =
    this.route.snapshot.paramMap.get('id');

    if(id){

      this.isEditMode = true;

      this.allocationId = +id;

      this.loadAllocation();

    }

  }

  loadEmployees(){

    this.employeeService
    .getEmployees()
    .subscribe({

      next:(data)=>{

        this.employees = data;

      }

    });

  }

  loadProjects(){

    this.projectService
    .getProjects()
    .subscribe({

      next:(data)=>{

        this.projects = data;

      }

    });

  }

  loadAllocation(){

    this.allocationService
    .getAllocationById(
      this.allocationId
    )
    .subscribe({

      next:(data)=>{

        this.allocationForm
        .patchValue(data);

      }

    });

  }

  onSubmit(){

    if(
      this.allocationForm.invalid
    ) return;

    if(this.isEditMode){

      this.allocationService
      .updateAllocation(

        this.allocationId,

        this.allocationForm.value

      )
      .subscribe({

        next:()=>{

          alert(
            'Allocation Updated Successfully'
          );

          this.router.navigate([
            '/allocations'
          ]);

        }

      });

    }

    else{

      this.allocationService
      .createAllocation(
        this.allocationForm.value
      )
      .subscribe({

        next:()=>{

          alert(
            'Allocation Added Successfully'
          );

          this.router.navigate([
            '/allocations'
          ]);

        }

      });

    }

  }

  goBack(){

    this.router.navigate([
      '/allocations'
    ]);

  }

}
