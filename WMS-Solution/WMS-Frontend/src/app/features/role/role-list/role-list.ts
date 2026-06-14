import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';

import { Router } from '@angular/router';

import { RoleService }
from '../../../core/services/role.service';

import { Role }
from '../../../core/models/role';

@Component({
  selector: 'app-role-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './role-list.html',
  styleUrl: './role-list.scss'
})
export class RoleList
implements OnInit {

  roles: Role[] = [];

  constructor(

    private roleService:
    RoleService,

    private router: Router

  ) {}

  ngOnInit(): void {

    this.loadRoles();

  }

  loadRoles() {

    this.roleService
      .getRoles()
      .subscribe({

        next: (data) => {

          this.roles = data;

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  addRole() {

    this.router.navigate(
      ['/roles/add']
    );

  }

  editRole(id:number){

    this.router.navigate(
      ['/roles/edit',id]
    );

  }

  deleteRole(id:number){

    if(confirm(
      'Delete Role?'
    )){

      this.roleService
      .deleteRole(id)
      .subscribe({

        next:()=>{

          this.loadRoles();

        }

      });

    }

  }

}