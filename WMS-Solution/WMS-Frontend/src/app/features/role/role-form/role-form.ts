import { Component, OnInit } from '@angular/core';

import {
FormBuilder,
FormGroup,
Validators,
ReactiveFormsModule
}
from '@angular/forms';

import { CommonModule }
from '@angular/common';

import {
Router,
ActivatedRoute
}
from '@angular/router';

import { RoleService }
from '../../../core/services/role.service';

@Component({
  selector:'app-role-form',
  standalone:true,
  imports:[
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl:'./role-form.html',
  styleUrl:'./role-form.scss'
})
export class RoleForm
implements OnInit {

  roleForm!: FormGroup;

  roleId = 0;

  isEditMode = false;

  constructor(

    private fb: FormBuilder,

    private roleService:
    RoleService,

    private router: Router,

    private route:
    ActivatedRoute

  ){}

  ngOnInit(): void {

    this.roleForm =
    this.fb.group({

      roleName:[
        '',
        Validators.required
      ],

      description:[
        ''
      ]

    });

    const id =
    this.route.snapshot
    .paramMap
    .get('id');

    if(id){

      this.isEditMode = true;

      this.roleId = +id;

      this.loadRole();

    }

  }

  loadRole(){

    this.roleService
    .getRoleById(
      this.roleId
    )
    .subscribe({

      next:(data)=>{

        this.roleForm
        .patchValue(data);

      }

    });

  }

  onSubmit(){

    if(
      this.roleForm.invalid
    ) return;

    if(this.isEditMode){

      this.roleService
      .updateRole(

        this.roleId,

        this.roleForm.value

      )
      .subscribe({

        next:()=>{

          alert(
            'Role Updated Successfully'
          );

          this.router.navigate([
            '/roles'
          ]);

        }

      });

    }

    else{

      this.roleService
      .createRole(
        this.roleForm.value
      )
      .subscribe({

        next:()=>{

          alert(
            'Role Added Successfully'
          );

          this.router.navigate([
            '/roles'
          ]);

        }

      });

    }

  }

  goBack(){

    this.router.navigate([
      '/roles'
    ]);

  }

}