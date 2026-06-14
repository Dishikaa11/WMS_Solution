import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';

import { DashboardService }
from '../../../core/services/dashboard.service';

@Component({

  selector:'app-employee-dashboard',

  standalone:true,

  imports:[
    CommonModule
  ],

  templateUrl:
  './employee-dashboard.html',

  styleUrl:
  './employee-dashboard.scss'

})

export class EmployeeDashboard
implements OnInit {

  dashboard:any;

  constructor(

    private dashboardService:
    DashboardService

  ){}

  ngOnInit(): void {

    this.loadDashboard();

  }

  loadDashboard(){

    this.dashboardService
    .getEmployeeDashboard()
    .subscribe({

      next:(data)=>{

        this.dashboard = data;

      }

    });

  }

}