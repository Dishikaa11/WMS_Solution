import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';

import { Router } from '@angular/router';

import {
EmployeeProjectAllocationService
}
from '../../../core/services/allocation.service';

import {
EmployeeProjectAllocation
}
from '../../../core/models/EmployeeAllocation';

@Component({
  selector:'app-employee-allocation-list',
  standalone:true,
  imports:[
    CommonModule
  ],
  templateUrl:'./allocation-list.html',
  styleUrl:'./allocation-list.scss'
})
export class EmployeeAllocationList
implements OnInit {

  allocations:
  EmployeeProjectAllocation[] = [];

  constructor(

    private service:
    EmployeeProjectAllocationService,

    private router: Router

  ){}

  ngOnInit(): void {

    this.loadAllocations();

  }

  loadAllocations(){

    this.service
    .getAllocations()
    .subscribe({

      next:(data)=>{

        this.allocations = data;

      },

      error:(err)=>{

        console.log(err);

      }

    });

  }

  addAllocation(){

    this.router.navigate([
      '/allocations/add'
    ]);

  }

  editAllocation(id:number){

    this.router.navigate([
      '/allocations/edit',
      id
    ]);

  }

  deleteAllocation(id:number){

    if(confirm(
      'Delete Allocation?'
    )){

      this.service
      .deleteAllocation(id)
      .subscribe({

        next:()=>{

          this.loadAllocations();

        },

        error:(err)=>{

          console.log(err);

        }

      });

    }

  }

}