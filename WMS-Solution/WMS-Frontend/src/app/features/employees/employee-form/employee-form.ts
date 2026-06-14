import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { EmployeeService } from '../../../core/services/employee.service';
import { DepartmentService } from '../../../core/services/department.service';
import { Department } from '../../../core/models/department';
import { OnInit } from '@angular/core';
import { RoleService } from '../../../core/services/role.service';
import { Role } from '../../../core/models/role';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employee-form.html',
  styleUrl: './employee-form.scss',
})
export class EmployeeForm
implements OnInit {

  employeeForm: FormGroup;
  departments: Department[] = [];
  roles: Role[] = [];
  employeeId = 0;
  isEditMode = false;

 constructor(
  private fb: FormBuilder,
  private router: Router,
  private route: ActivatedRoute,
  private employeeService: EmployeeService,
  private departmentService: DepartmentService,
  private roleService: RoleService
) {

    this.employeeForm = this.fb.group({

      firstName: ['', Validators.required],

      lastName: ['', Validators.required],

      email: ['', [Validators.required, Validators.email]],

      phoneNumber: ['', Validators.required],

      gender: ['', Validators.required],

      dob: ['', Validators.required],

      doj: ['', Validators.required],

      departmentId: ['', Validators.required],

      roleId: ['', Validators.required],

      status: ['Active']

    });

  }

  ngOnInit(): void {

  this.loadDepartments();

  this.loadRoles();

  const id =
  this.route.snapshot.paramMap.get('id');

  if(id){

    this.isEditMode = true;

    this.employeeId = +id;

    this.employeeService
    .getEmployeeById(this.employeeId)
    .subscribe({

      next:(employee)=>{

        this.employeeForm.patchValue(employee);

      }

    });

  }

}
  onSubmit() {

  if (!this.employeeForm.valid)
    return;

  if(this.isEditMode){

    this.employeeService
    .updateEmployee(

      this.employeeId,

      this.employeeForm.value

    )
    .subscribe({

      next:()=>{

        alert(
          'Employee Updated Successfully'
        );

        this.router.navigate([
          '/employees'
        ]);

      },

      error:(err)=>{

        console.log(err);

        alert(
          'Error Updating Employee'
        );

      }

    });

  }

  else{

    this.employeeService
    .createEmployee(
      this.employeeForm.value
    )
    .subscribe({

      next:()=>{

        alert(
          'Employee Added Successfully'
        );

        this.router.navigate([
          '/employees'
        ]);

      },

      error:(err)=>{

        console.log(err);

        alert(
          'Error Saving Employee'
        );

      }

    });

  }

}
  goBack() {
    this.router.navigate(['/employees']);
  }

  loadDepartments(){

  this.departmentService
  .getDepartments()
  .subscribe({

    next:(data)=>{

      console.log(
        'Departments =>',
        data
      );

      this.departments = data;

    },

    error:(err)=>{

      console.log(
        'Department Error =>',
        err
      );

    }

  });

}
loadRoles(){

  this.roleService
  .getRoles()
  .subscribe({

    next:(data)=>{

      console.log(
        'Roles =>',
        data
      );

      this.roles = data;

    },

    error:(err)=>{

      console.log(
        'Role Error =>',
        err
      );

    }

  });

}

}
