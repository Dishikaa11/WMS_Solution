import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { BaseChartDirective } from 'ng2-charts';
import { DashboardService } from '../../../core/services/dashboard.service';
import {Chart, registerables } from 'chart.js';
import { AuditLogService } from '../../../core/services/audit-log.service';
import { AuditLog } from '../../../core/models/audit-log';





@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    BaseChartDirective
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {

  totalEmployees = 0;

  totalDepartments = 0;

  totalProjects = 0;

  pendingLeaves = 0;

  leavePieChartData: any;

  projectChartData: any;
  auditLogs: AuditLog[] = [];

  constructor(
    private dashboardService:DashboardService,
    private auditLogService:AuditLogService 

    // private cdr:
    // ChangeDetectorRef
  ) {}

  ngOnInit(): void {

        Chart.register(
        ...registerables
      );

      this.loadDashboard();
      this.loadAuditLogs();

  }

  loadDashboard(): void {

    this.dashboardService
      .getSummary()
      .subscribe({

        next: (response) => {

          // console.log(
          //   'Dashboard Response:',
          //   response
          // );

          this.totalEmployees =
            response.totalEmployees;

          this.totalDepartments =
            response.totalDepartments;

          this.totalProjects =
            response.totalProjects;

          this.pendingLeaves =
            response.pendingLeaves;

          // Leave Pie Chart

          this.leavePieChartData = {

            labels: [

              'Employees',

              'Departments',

              'Projects',

              'Pending Leaves'

            ],

            datasets: [

              {

                data: [

                  response.totalEmployees,

                  response.totalDepartments,

                  response.totalProjects,

                  response.pendingLeaves

                ]

              }

            ]

          };

          // Project Bar Chart

          this.projectChartData = {

            labels: [

              'Projects'

            ],

            datasets: [

              {

                label:
                'Total Projects',

                data: [

                  response.totalProjects

                ]

              }

            ]

          };

          // this.cdr.detectChanges();

        },

        error: (error) => {

          console.log(error);

        }

      });

  }
  loadAuditLogs(){

  this.auditLogService
  .getLogs()
  .subscribe({

    next:(data)=>{

      this.auditLogs =
      data
      .sort(

        (a,b)=>

        new Date(
          b.createdOn
        ).getTime()

        -

        new Date(
          a.createdOn
        ).getTime()

      )
      .slice(0,10);

    }

  });

}

}