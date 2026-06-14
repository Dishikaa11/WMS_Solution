import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';

import { AuditLogService }
from '../../../core/services/audit-log.service';

import { AuditLog }
from '../../../core/models/audit-log';

@Component({
  selector:'app-audit-log-list',
  standalone:true,
  imports:[
    CommonModule
  ],
  templateUrl:'./audit-log-list.html',
  styleUrl:'./audit-log-list.scss'
})
export class AuditLogList
implements OnInit {

  logs: AuditLog[] = [];

  constructor(

    private auditService:
    AuditLogService

  ){}

  ngOnInit(): void {

    this.loadLogs();

  }

  loadLogs(){

    this.auditService
    .getLogs()
    .subscribe({

      next:(data)=>{

        this.logs = data;

      },

      error:(err)=>{

        console.log(err);

      }

    });

  }

}
